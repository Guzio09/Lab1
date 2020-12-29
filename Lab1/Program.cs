using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                pTTS.SetOutputToDefaultAudioDevice();
                pTTS.Speak("Witam w kalkulatorze");
                // Ustawienie języka rozpoznawania:
                CultureInfo ci = new CultureInfo("pl-PL");

                // Utworzenie "silnika" rozpoznawania:
                pSRE = new SpeechRecognitionEngine(ci);

                // Ustawienie domyślnego urządzenia wejściowego:
                pSRE.SetInputToDefaultAudioDevice();

                // Przypisanie obsługi zdarzenia realizowanego po rozpoznaniu wypowiedzi
                zgodnej z gramatyką:
                pSRE.SpeechRecognized += PSRE_SpeechRecognized;
                // -------------------------------------------------------------------------
                // Budowa gramatyki numer 1 - POLECENIA SYSTEMOWE
                // Budowa gramatyki numer 1 - określenie komend:
                Choices stopChoice = new Choices();
                stopChoice.Add("Stop");
                stopChoice.Add("Pomoc");

                // Budowa gramatyki numer 1 - definiowanie składni gramatyki:
                GrammarBuilder buildGrammarSystem = new GrammarBuilder();
                buildGrammarSystem.Append(stopChoice);

                // Budowa gramatyki numer 1 - utworzenie gramatyki:
                Grammar grammarSystem = new Grammar(buildGrammarSystem); //
                                                                         // -------------------------------------------------------------------------
                                                                         // Budowa gramatyki numer 2 - POLECENIA DLA PROGRAMU
                                                                         // Budowa gramatyki numer 2 - określenie komend:
                Choices chNumbers = new Choices(); //możliwy wybór słów
                string[] numbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7",
"8", "9" };
                chNumbers.Add(numbers);
                // Budowa gramatyki numer 2 - definiowanie składni gramatyki:
                GrammarBuilder grammarProgram = new GrammarBuilder();
                grammarProgram.Append("Oblicz");
                grammarProgram.Append(chNumbers);
                grammarProgram.Append("plus");
                grammarProgram.Append(chNumbers);

                // Budowa gramatyki numer 2 - utworzenie gramatyki:
                Grammar g_WhatIsXplusY = new Grammar(grammarProgram); //gramatyka
                                                                      // -------------------------------------------------------------------------
                                                                      // Załadowanie gramatyk:
                pSRE.LoadGrammarAsync(g_WhatIsXplusY);
                pSRE.LoadGrammarAsync(grammarSystem);
                // Ustaw rozpoznawanie przy wykorzystaniu wielu gramatyk:
                pSRE.RecognizeAsync(RecognizeMode.Multiple);
                // -------------------------------------------------------------------------
                Console.WriteLine("\nAby zakonczyć działanie programu powiedz 'STOP'\n");
                while (speechOn == true) {; } //pętla w celu uniknięcia zamknięcia programu
                Console.WriteLine("\tWCIŚNIJ <ENTER> aby wyjść z programu\n");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
