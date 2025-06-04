using CourseManagement.Core.Models;

namespace CourseManagement.Core.Utilities
{
    public static class IdGenerator
    {
        public static string GenerateCourseId()
        {
            // Cách 1: (từ Guid)
            var guid = Guid.NewGuid().ToString("N").ToUpper();
            return $"COURSE_{guid.Substring(0, 8)}";

            // Hoặc Cách 2: (random bytes)
            // var bytes = new byte[4];
            // using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            // {
            //     rng.GetBytes(bytes);
            // }
            // return $"COURSE_{BitConverter.ToString(bytes).Replace("-", "")}";
        }
        
        public static string GenerateNextCategoryId(List<Category> categories)
        {
            const string prefix = "CAT_";

            var numbers = categories
                .Where(c => c.CategoryId.StartsWith(prefix))
                .Select(c =>
                {
                    var numPart = c.CategoryId.Substring(prefix.Length);
                    return int.TryParse(numPart, out int n) ? n : 0;
                })
                .ToList();

            int nextNumber = numbers.Count > 0 ? numbers.Max() + 1 : 1;

            return $"{prefix}{nextNumber.ToString("D3")}";
        }


    }
}