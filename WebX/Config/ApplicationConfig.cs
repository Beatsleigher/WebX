using System;

namespace WebX.Config {
    
    using Newtonsoft.Json;
    
    using System.IO;
    using System.Net;
    
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    
    /// <summary>
    /// Application config manager.
    /// </summary>
    public class ApplicationConfig {
        
        #region Private members

        [YamlIgnore]
        private const string appConfTemplateFilename = "appconf.template.yml";
        [YamlIgnore]
        private const string appConfFilename = "appconf.yml";

        [YamlIgnore]
        private Deserializer yamlDeserialiser;
        #endregion
        
        #region Constructor (Singleton)

        /// <summary>
        /// Default constructor.
        /// </summary>
        private ApplicationConfig() {
            yamlDeserialiser = new DeserializerBuilder().WithNamingConvention(new UnderscoredNamingConvention())
                                                        .Build() as Deserializer;
        }
        
        // Singleton
        [YamlIgnore]
        private static ApplicationConfig instance;
        /// <summary>
        /// Gets the singleton instance of this class.
        /// </summary>
        [YamlIgnore]
        public static ApplicationConfig Instance => instance ?? (instance = new ApplicationConfig()); 
        #endregion
        
        /// <summary>
        /// Gets the logging configuration.
        /// </summary>
        public LoggerConfig LoggerConfig { get; private set; }
        
        /// <summary>
        /// Gets the most basic application configuration.
        /// </summary>
        public WebXConfig WebXConfig { get; private set; }

        /// <summary>
        /// Gets the web config directory.
        /// </summary>
        [YamlIgnore]
        public string WebsiteConfigDir => Path.Combine(WebXConfig?.ConfigDir, "www-conf");

        /// <summary>
        /// Loads the mainest of application configs.
        /// </summary>
        internal async void LoadConfig() {
            // First check if the correct files exist.
            var localConfTemplateFile = Path.Combine(".", appConfTemplateFilename);
            var localConfFile = Path.Combine(".", appConfFilename);
            
            if (!File.Exists(localConfTemplateFile)) {
                // Write template file to disk
                using (var resourceStream = GetType().Assembly.GetManifestResourceStream(
                        string.Join(".", nameof(WebX.Config), "default", 
                                                               appConfTemplateFilename)))
                using (var resourceStreamReader = new StreamReader(resourceStream))
                using (var fileStream = File.Open(localConfTemplateFile, FileMode.CreateNew))
                using (var fileWriter = new StreamWriter(fileStream)) {
                    string line;

                    while (resourceStreamReader.Peek() != -1) {
                        line = await resourceStreamReader.ReadLineAsync();
                        await fileWriter.WriteLineAsync(line);
                    }
                }
            }

            // First see if the config file exists at all.
            if (!File.Exists(localConfFile)) {
                File.Copy(localConfTemplateFile, localConfFile);
            }
            
            // Now check if we can deserialise the config file.
            try {
                using (var fStream = File.OpenText(localConfFile)) {
                    var deserialisedConfig = yamlDeserialiser.Deserialize<ApplicationConfig>(fStream);
                    LoggerConfig = deserialisedConfig.LoggerConfig;
                    WebXConfig = deserialisedConfig.WebXConfig;
                }
            } catch (Exception ex) {
                Console.WriteLine("--ERROR-- COULD NOT READ {0}!", localConfFile);
                if (Environment.UserInteractive) {
                    // Prompt to re-create file
                    Console.Write("Re-create {0}? [Y/n]", localConfFile);
                    var answer = Console.ReadLine();
                    if (string.IsNullOrEmpty(answer) || answer.Trim().ToLowerInvariant() == "y") {
                        File.Move(localConfFile, string.Join(".", localConfFile, "broken"));
                        LoadConfig();
                    } else {
                        Console.WriteLine("WebX CANNOT CONTINUE!");
                        Environment.Exit(Program.BadConfig);
                    }
                    
                    return;
                }
            }

            LoadSiteConfigs();
        }

        /// <summary>
        /// Loads website configurations.
        /// </summary>
        internal async void LoadSiteConfigs() {
        }

    }
}