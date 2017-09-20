﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Google.Apis.Services;
using Google.Apis.Translate.v2;
using TranslationsResource = Google.Apis.Translate.v2.Data.TranslationsResource;
using System.ComponentModel;
using System.Reflection;

namespace GoogleApi
{
    class Program
    {
        #region User Input

        /// <summary>User input for this example.</summary>
        [Description("input")]
        public class TranslateInput
        {
            [Description("text to translate")]
            public string SourceText = "Who ate my candy?";
            [Description("target language")]
            public string TargetLanguage = "fr";
        }

        /// <summary>
        /// Creates a new instance of T and fills all public fields by requesting input from the user.
        /// </summary>
        /// <typeparam name="T">Class with a default constructor</typeparam>
        /// <returns>Instance of T with filled in public fields</returns>
        public static T CreateClassFromUserinput<T>()
        {
            var type = typeof(T);

            // Create an instance of T
            T settings = Activator.CreateInstance<T>();

            Console.WriteLine("Please enter values for the {0}:", GetDescriptiveName(type));

            // Fill in parameters
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                object value = field.GetValue(settings);

                // Let the user input a value
                RequestUserInput(GetDescriptiveName(field), ref value, field.FieldType);

                field.SetValue(settings, value);
            }

            Console.WriteLine();
            return settings;
        }

        /// <summary>
        /// Tries to return a descriptive name for the specified member info. It uses the DescriptionAttribute if 
        /// available.
        /// </summary>
        /// <returns>Description from DescriptionAttriute or name of the MemberInfo</returns>
        public static string GetDescriptiveName(MemberInfo info)
        {
            // If available, return the description set in the DescriptionAttribute.
            foreach (DescriptionAttribute attribute in info.GetCustomAttributes(typeof(DescriptionAttribute), true))
            {
                return attribute.Description;
            }

            // Otherwise: return the name of the member.
            return info.Name;
        }

        /// <summary>Requests an user input for the specified value.</summary>
        /// <param name="name">Name to display.</param>
        /// <param name="value">Default value, and target value.</param>
        /// <param name="valueType">Type of the target value.</param>
        private static void RequestUserInput(string name, ref object value, Type valueType)
        {
            do
            {
                Console.Write("\t{0}: ", name);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    // No change required, use default value.
                    return;
                }

                try
                {
                    value = Convert.ChangeType(input, valueType);
                    return;
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("Please enter a valid value!");
                }
            } while (true); // Run this loop until the user gives a valid input.
        }

        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Translate Sample");
            Console.WriteLine("================");

            try
            {
                new Program().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private async Task Run()
        {
            var key = "AIzaSyBlBTcFpepfTQsZSl-aC8-EZp1ksYgvyNs";

            // Ask for the user input.
            TranslateInput input = CreateClassFromUserinput<TranslateInput>();

            // Create the service.
            var service = new TranslateService(new BaseClientService.Initializer()
            {
                ApiKey = key,
                ApplicationName = "Translate API Sample"
            });

            // Execute the first translation request.
            Console.WriteLine("Translating to '" + input.TargetLanguage + "' ...");

            string[] srcText = new[] { "Hello world!", input.SourceText };
            var response = await service.Translations.List(srcText, input.TargetLanguage).ExecuteAsync();
            var translations = new List<string>();

            foreach (TranslationsResource translation in response.Translations)
            {
                translations.Add(translation.TranslatedText);
                Console.WriteLine("translation :" + translation.TranslatedText);
            }

            // Translate the text (back) to English.
            Console.WriteLine("Translating to English ...");

            response = service.Translations.List(translations, "en").Execute();
            foreach (TranslationsResource translation in response.Translations)
            {
                Console.WriteLine("translation :" + translation.TranslatedText);
            }
        }
    }
}
