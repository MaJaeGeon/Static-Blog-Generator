using System;

namespace StaticBlogGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator(
                basePath: @"C:\Users\akwor\Documents\GitHub\StaticBlogGenerator\StaticBlogGenerator\Public\",
                staticModelsPath: @"C:\Users\akwor\Documents\GitHub\StaticBlogGenerator\StaticBlogGenerator\StaticModels\"
            );

            generator.Initialize();
            generator.Build();
        }
    }
}
