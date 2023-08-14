using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Sandbox_Program.Models;
using Sandbox_Program.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI;
using Windows.UI.WebUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;


namespace Sandbox_Program.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly DataService _Services;

        //TODO finish it          xaml code    IsOn="{Binding IsSwitchOn, Mode=TwoWay}  RequestedTheme="{Binding SelectedTheme}""
        /*private bool _isSwitchOn;
        public bool IsSwitchOn
        {
            get => _isSwitchOn;
            set
            {
                if (_isSwitchOn != value)
                {
                    _isSwitchOn = value;
                    OnPropertyChanged();
                }
            }
        }*/
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SendCommand { get; private set; }

        private string _outputText;
        public string OutputText
        {
            get { return _outputText; }
            set { SetProperty(ref _outputText, value); }
        }

        private LanguageOption _selectedOption;
        public LanguageOption SelectedOption
        {
            get { return _selectedOption; }
            set { SetProperty(ref _selectedOption, value); }
        }

        private int _memoryLimit;
        public int MemoryLimit
        {
            get { return _memoryLimit; }
            set { SetProperty(ref _memoryLimit, value); }
        }

        private int _timeLimit;
        public int TimeLimit
        {
            get { return _timeLimit; }
            set { SetProperty(ref _timeLimit, value); }
        }

        public MainPageViewModel() 
        {
            _Services = new DataService();
            SendCommand = new RelayCommand(ExecuteSendCommand);
            LanguageOptions = new ObservableCollection<LanguageOption>
                {
                    new LanguageOption { Code = "C",               Name = "C" ,                 BaseCode = "#include <stdio.h>\r\n\r\nint main(){\r\n\tint num;\r\n\tscanf(\"%d\", &num);              \t             \r\n\tprintf(\"Input number is %d.\\n\", num);      \r\n}"},
                    new LanguageOption { Code = "CPP14",           Name = "C++14",              BaseCode = "#include <iostream>\r\nusing namespace std;\r\nint main() {\r\n\tint num;\r\n\tcin >> num;    \r\n\tcout << \"Input number is \" << num << endl;\t\r\n}" },
                    new LanguageOption { Code = "CPP17",           Name = "C++17",              BaseCode = "#include <iostream>\r\nusing namespace std;\r\nint main() {\r\n\tint num;\r\n\tcin >> num;    \r\n\tcout << \"Input number is \" << num << endl;\t\r\n}" },
                    new LanguageOption { Code = "CLOJURE",         Name = "Clojure",            BaseCode = "(comment \"\r\n;; Sample code to perform I/O:\r\n\r\n(def x (read-line))                 ; Reading input from STDIN\r\n(println (str \"Hi, \" x \".\"))        ; Writing output to STDOUT\r\n\r\n;; Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n\")\r\n\r\n; Write your code here" },
                    new LanguageOption { Code = "CSHARP",          Name = "C#",                 BaseCode = "/*\r\n// Sample code to perform I/O:\r\n\r\nauto name = strip(stdin.readln());      // Reading input from STDIN\r\nwriteln(\"Hi, \", name, \".\\n\");           // Writing output to STDOUT\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*/\r\n\r\n// Write your code here" },
                    new LanguageOption { Code = "GO",              Name = "Go",                 BaseCode = "/*\r\n// Sample code to perform I/O:\r\n\r\nfmt.Scanf(\"%s\", &myname)            // Reading input from STDIN\r\nfmt.Println(\"Hello\", myname)        // Writing output to STDOUT\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*/\r\n\r\n// Write your code here" },
                    new LanguageOption { Code = "HASKELL",         Name = "Haskell",            BaseCode = "-- Sample code to perform I/O:\r\n\r\n-- main = do\r\n--     name <- getLine                         -- Reading input from STDIN\r\n--     putStrLn (\"Hi, \" ++ name ++ \".\")        -- Writing output to STDOUT\r\n\r\n-- Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n\r\n-- Write your code here" },
                    new LanguageOption { Code = "JAVA8",           Name = "Java 8",             BaseCode = "/* IMPORTANT: Multiple classes and nested static classes are supported */\r\n\r\n/*\r\n * uncomment this if you want to read input.\r\n//imports for BufferedReader\r\nimport java.io.BufferedReader;\r\nimport java.io.InputStreamReader;\r\n\r\n//import for Scanner and other utility classes\r\nimport java.util.*;\r\n*/\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n\r\nclass TestClass {\r\n    public static void main(String args[] ) throws Exception {\r\n        /* Sample code to perform I/O:\r\n         * Use either of these methods for input\r\n\r\n        //BufferedReader\r\n        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));\r\n        String name = br.readLine();                // Reading input from STDIN\r\n        System.out.println(\"Hi, \" + name + \".\");    // Writing output to STDOUT\r\n\r\n        //Scanner\r\n        Scanner s = new Scanner(System.in);\r\n        String name = s.nextLine();                 // Reading input from STDIN\r\n        System.out.println(\"Hi, \" + name + \".\");    // Writing output to STDOUT\r\n\r\n        */\r\n\r\n        // Write your code here\r\n\r\n    }\r\n}" },
                    new LanguageOption { Code = "JAVA14",          Name = "Java 14",            BaseCode = "import java.io.BufferedReader;\r\nimport java.io.InputStreamReader;\r\nimport java.util.*;\r\nclass TestClass {\r\n    public static void main(String args[] ) throws Exception {\r\n        //BufferedReader\r\n        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));\r\n        String name = br.readLine();                // Reading input from STDIN\r\n        System.out.println(\"Hi, \" + name + \".\");    // Writing output to STDOUT\r\n    }\r\n}" },
                    new LanguageOption { Code = "JAVASCRIPT_NODE", Name = "JavaScript(Nodejs)", BaseCode = "/*\r\n// Sample code to perform I/O:\r\n\r\nprocess.stdin.resume();\r\nprocess.stdin.setEncoding(\"utf-8\");\r\nvar stdin_input = \"\";\r\n\r\nprocess.stdin.on(\"data\", function (input) {\r\n    stdin_input += input;                               // Reading input from STDIN\r\n});\r\n\r\nprocess.stdin.on(\"end\", function () {\r\n   main(stdin_input);\r\n});\r\n\r\nfunction main(input) {\r\n    process.stdout.write(\"Hi, \" + input + \".\\n\");       // Writing output to STDOUT\r\n}\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*/\r\n\r\n// Write your code here" },
                    new LanguageOption { Code = "KOTLIN",          Name = "Kotlin",             BaseCode = "/*\r\n// Sample code to perform I/O:\r\n\r\nval name = readLine()!!             // Reading input from STDIN\r\nprintln(\"Hi, \" + name + \".\");       // Writing output to STDOUT\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*/\r\n\r\n// Write your code here" },
                    new LanguageOption { Code = "OBJECTIVEC",      Name = "Objective C",        BaseCode = "/*\r\n// Sample code to perform I/O:\r\n\r\n# include<stdio.h>\r\n\r\nint main(){\r\n    int num;\r\n\r\n    scanf(\"%s\", &num);                              // Reading input from STDIN\r\n    printf(\"Input number is %s.\\n\", &num);           // Writing output to STDOUT\r\n\r\n}\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*/\r\n\r\n// Write your code here" },
                    new LanguageOption { Code = "PASCAL",          Name = "Pascal",             BaseCode = "(*\r\n// Sample code to perform I/O:\r\n\r\nreadln (name);                      // Reading input from STDIN\r\nwriteln ('Hi, ', name, '.');        // Writing output to STDOUT\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*)\r\n\r\n// Write your code here\r\n" },
                    new LanguageOption { Code = "PERL",            Name = "Perl",               BaseCode = "=comment\r\n# Sample code to perform I/O:\r\n\r\nmy $name = <STDIN>;             # Reading input from STDIN\r\nprint \"Hi, $name.\\n\";           # Writing output to STDOUT\r\n\r\n# Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n=cut\r\n\r\n# Write your code here" },
                    new LanguageOption { Code = "PHP",             Name = "PHP",                BaseCode = "<?php\r\n/*\r\n// Sample code to perform I/O:\r\n\r\nfscanf(STDIN, \"%s\\n\", $name);           // Reading input from STDIN\r\necho \"Hi, \".$name.\".\\n\";                // Writing output to STDOUT\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*/\r\n\r\n// Write your code here\r\n\r\n?>" },
                    new LanguageOption { Code = "PYTHON",          Name = "Python 2",           BaseCode = "name = raw_input()          # Reading input from STDIN\r\nprint('Hi, %s.' % name)      # Writing output to STDOUT" },
                    new LanguageOption { Code = "PYTHON3",         Name = "Python 3",           BaseCode = "name = input()                  # Reading input from STDIN\r\nprint('Hi, %s.' % name)         # Writing output to STDOUT" },
                    new LanguageOption { Code = "PYTHON3_8",       Name = "Python 3.8",         BaseCode = "name = input()                  # Reading input from STDIN\r\nprint('Hi, %s.' % name)         # Writing output to STDOUT" },
                    new LanguageOption { Code = "R",               Name = "R",                  BaseCode = "cat(\"Hello World!\")" },
                    new LanguageOption { Code = "RUBY",            Name = "Ruby",               BaseCode = "=begin\r\n# Sample code to perform I/O:\r\n\r\nname = gets.chomp                # Reading input from STDIN\r\nprint \"Hi, #{name}.\\n\"           # Writing output to STDOUT\r\n\r\n# Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n=end\r\n\r\n# Write your code here" },
                    new LanguageOption { Code = "RUST",            Name = "Rust",               BaseCode = "/*\r\n// Sample code to perform I/O:\r\n\r\nuse std::io;\r\nuse std::io::prelude::*;\r\n\r\nfn main() {\r\n    let mut name = String::new();\r\n    io::stdin().read_line(&mut name).unwrap();          // Reading input from STDIN\r\n    println!(\"Hi, {}.\", name);                          // Writing output to STDOUT\r\n}\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*/\r\n\r\n// Write your code here" },
                    new LanguageOption { Code = "SCALA",           Name = "Scala",              BaseCode = "/*\r\n// Sample code to perform I/O:\r\n\r\nval name = scala.io.StdIn.readLine()        // Reading input from STDIN\r\nprintln(\"Hi, \" + name + \".\")                // Writing output to STDOUT\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*/\r\n\r\n// Write your code here" },
                    new LanguageOption { Code = "SWIFT",           Name = "Swift",              BaseCode = "/*\r\n// Sample code to perform I/O:\r\n\r\nlet name = readLine()                           // Reading input from STDIN\r\nprint(\"Hi, \", name!, \".\\n\", separator: \"\")      // Writing output to STDOUT\r\n\r\n// Warning: Printing unwanted or ill-formatted data to output will cause the test cases to fail\r\n*/\r\n\r\n// Write your code here" },
                    new LanguageOption { Code = "TYPESCRIPT",      Name = "TypeScript",         BaseCode = "console.log(\"Hello World!\")" }
                };
            Problems = new ObservableCollection<Problem>
            {
                new Problem { Name = "Hello World",     SampleOutput = "Hello World",   SampleInput = "",   Detail = "Write a code, output \"Hello World\"" },
                new Problem { Name = "First problem",   SampleOutput = "4",             SampleInput = "4",  Detail = "Write a code that takes a value and prints it" },
                new Problem { Name = "Second problem",  SampleOutput = "6",             SampleInput = "4",  Detail = "Write a code that takes a value in X and use this furmula to output X+2" }
            };
        }

        private Problem _selectedProblem;
        public Problem SelectedProblem
        {
            get { return _selectedProblem; }
            set { SetProperty(ref _selectedProblem, value); }
        }

        public ObservableCollection<LanguageOption> LanguageOptions { get; set; }
        public ObservableCollection<Problem> Problems { get; set; }












        private async void ExecuteSendCommand()
        {
            PostModel postModel = JsonConvert.DeserializeObject<PostModel>(await _Services.PostCodeAsync(SelectedOption.BaseCode, SelectedOption.Code, MemoryLimit, TimeLimit, SelectedProblem.SampleInput)); //TODO input
            StatusModel statusModel = JsonConvert.DeserializeObject<StatusModel>(await _Services.GetStatusAsync(postModel.he_id));
            string a = await _Services.GetResultAsync(statusModel.result.run_status.output);
            if (a == SelectedProblem.SampleOutput + "\n") 
            {
                OutputText = "YOU WIN \n" + SelectedProblem.SampleOutput;
            }
            else
            {
                OutputText = statusModel.request_status.code;
            }
            
        }

















        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
