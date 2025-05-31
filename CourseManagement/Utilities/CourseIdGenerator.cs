namespace CourseManagement.Utilities
{
    public static class CourseIdGenerator
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
    }
}