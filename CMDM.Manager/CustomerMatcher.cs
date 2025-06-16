using System.Text.RegularExpressions;
using CMDM.Core.Models;
using FuzzySharp;

namespace CMDM.Manager
{
    public static class CustomerMatcher
    {
        private static readonly Dictionary<string, string> CountryMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "uk", "united kingdom" },
            { "gb", "united kingdom" },
            { "united kin", "united kingdom" },
            { "england", "united kingdom" },
            { "scotland", "united kingdom" },
            { "wales", "united kingdom" },
            { "northern ireland", "united kingdom" }
        };

        private static readonly Dictionary<string, string> NameMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "british water pvt ltd.", "british water pvt ltd" },
            { "british water", "british water pvt ltd" },
            { "british water pvt", "british water pvt ltd" },
            { "brw", "british water pvt ltd" },
            { "row electronics", "row Electronics"  },
            { "row electronic ltd", "row Electronics"  }
        };

        public static List<List<Customer>> GroupSimilarCustomers(List<Customer> customers)
        {
            var visited = new HashSet<string>();
            var groups = new List<List<Customer>>();

            foreach (var customer in customers)
            {
                if (visited.Contains(customer.CustCode))
                    continue;

                var group = new List<Customer> { customer };
                visited.Add(customer.CustCode);

                foreach (var other in customers)
                {
                    if (customer.CustCode == other.CustCode || visited.Contains(other.CustCode))
                        continue;

                    if (AreSimilar(customer, other))
                    {
                        group.Add(other);
                        visited.Add(other.CustCode);
                    }
                }

                groups.Add(group);
            }

            return groups;
        }

        private static bool AreSimilar(Customer a, Customer b)
        {
            string name1 = NormalizeName(a.Name);
            string name2 = NormalizeName(b.Name);

            string address1 = Normalize($"{a.Add01} {a.Add02}".Trim());
            string address2 = Normalize($"{b.Add01} {b.Add02}".Trim());

            string postcode1 = Normalize(a.PostCode);
            string postcode2 = Normalize(b.PostCode);

            string country1 = NormalizeCountry(a.Country);
            string country2 = NormalizeCountry(b.Country);

            double nameSim = LevenshteinSimilarity(name1, name2);
            double addressSim = LevenshteinSimilarity(address1, address2);
            double postcodeSim = LevenshteinSimilarity(postcode1, postcode2);
            double countrySim = LevenshteinSimilarity(country1, country2);

            double weightedScore = (nameSim * 0.4) + (addressSim * 0.3) + (postcodeSim * 0.2) + (countrySim * 0.1);

            return weightedScore >= 0.65;
        }

        private static double LevenshteinSimilarity(string s1, string s2)
        {
            if (string.IsNullOrWhiteSpace(s1) || string.IsNullOrWhiteSpace(s2))
                return 0.0;

            if (s1 == s2)
                return 1.0;

            int maxLen = Math.Max(s1.Length, s2.Length);
            if (maxLen == 0) return 1.0;

            var distance = Levenshtein.EditDistance(s1, s2);
            return 1.0 - (double)distance / maxLen;
        }

        private static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";
            input = input.ToLowerInvariant();
            input = Regex.Replace(input, @"[^a-z0-9\s]", "");
            return input.Trim();
        }

        private static string NormalizeCountry(string input)
        {
            var norm = Normalize(input);
            if (CountryMap.TryGetValue(norm, out var standard))
                return standard;
            return norm;
        }

        private static string NormalizeName(string input)
        {
            var norm = Normalize(input);
            if (NameMap.TryGetValue(norm, out var standard))
                return standard;
            return norm;
        }
    }
}
