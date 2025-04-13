using System.Collections;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using TI_lab2.Logic;

namespace TI_lab2
{
    public partial class MainWindow : Window
    {
        // Сохраняем путь к выбранному файлу
        private string selectedFilePath = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void tbxRegister_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxRegister != null && tbxLength != null)
            {
                tbxLength.Text = $"{tbxRegister.Text.Length}";
            }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                selectedFilePath = dlg.FileName;
            }
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessFile(encrypt: true);
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessFile(encrypt: false);
        }

        /// <summary>
        /// Основной метод обработки файла – выполняется шифрование или дешифрование в памяти.
        /// Результат сохраняется туда, куда укажет пользователь через диалог сохранения.
        /// После сохранения в RichTextBox'ах отображаются битовые представления исходного файла, сгенерированного ключа и результата.
        /// Если данных много (файл > 2*(2*RegisterLength) бит), выводятся только первые и последние 2*RegisterLength бит.
        /// </summary>
        /// <param name="encrypt">Если true – шифрование, иначе дешифрование.</param>
        private void ProcessFile(bool encrypt)
        {
            string registerState = tbxRegister.Text.Trim();
            if (string.IsNullOrEmpty(registerState))
            {
                MessageBox.Show("Введите начальное состояние регистра.");
                return;
            }
            if (!LSFR.ValidateRegisterState(registerState))
            {
                MessageBox.Show("Состояние регистра должно содержать ровно 29 символов, только 0 и 1.");
                return;
            }
            if (string.IsNullOrEmpty(selectedFilePath) || !File.Exists(selectedFilePath))
            {
                MessageBox.Show("Сначала выберите файл для обработки.");
                return;
            }

            try
            {
                string directory = Path.GetDirectoryName(selectedFilePath);
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(selectedFilePath);
                string extension = Path.GetExtension(selectedFilePath);
                string defaultFileName = filenameWithoutExt + (encrypt ? "_enc" : "_dec") + extension;

                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    Title = encrypt ? "Сохранить зашифрованный файл" : "Сохранить дешифрованный файл",
                    FileName = defaultFileName,
                    InitialDirectory = directory,
                    Filter = "Все файлы (*.*)|*.*"
                };

                if (saveDlg.ShowDialog() == true)
                {
                    LSFR.ProcessFile(selectedFilePath, saveDlg.FileName, registerState);

                    byte[] originalBytes = File.ReadAllBytes(selectedFilePath);
                    byte[] resultBytes = File.ReadAllBytes(saveDlg.FileName);

                    // Отображаем двоичное представление в RichTextBox'ах
                    rtbOpenText.Document.Blocks.Clear();
                    rtbOpenText.AppendText(GetPartialBitString(new BitArray(originalBytes)));

                    rtbKeyText.Document.Blocks.Clear();
                    rtbKeyText.AppendText(GetPartialBitString(LSFR.KeyStream));

                    rtbCipherText.Document.Blocks.Clear();
                    rtbCipherText.AppendText(GetPartialBitString(new BitArray(resultBytes)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обработке файла: " + ex.Message);
            }
        }

        /// <summary>
        /// Преобразует BitArray в строку с группировкой по 8 бит (1 байт) и разделителем.
        /// Если размер данных превышает 2 * (2*RegisterLength) бит (то есть 116 бит),
        /// выводятся только первые и последние 2*RegisterLength (58) бит.
        /// Это применяется как к исходным/обработанным файлам, так и к ключевому потоку.
        /// </summary>
        /// <param name="bits">Битовый массив</param>
        /// <returns>Строковое представление битов с разделителем между байтами</returns>
        private string GetPartialBitString(BitArray bits)
        {
            // 2 * длина регистра = 2 * 29 = 58 бит для отображения
            const int bitsToDisplay = 58;
            // Разделитель между байтами – можно заменить на "__", если необходимо.
            string delimiter = " ";
            StringBuilder sb = new StringBuilder();

            // Вспомогательная функция для форматирования заданного диапазона бит с группировкой по 8 бит.
            string FormatBits(int startIndex, int length)
            {
                StringBuilder group = new StringBuilder();
                int count = 0;
                for (int i = startIndex; i < startIndex + length; i++)
                {
                    group.Append(bits[i] ? "1" : "0");
                    count++;
                    // Если прошли 8 бит и это не конец заданного диапазона – добавить разделитель
                    if (count % 8 == 0 && count < length)
                        group.Append(delimiter);
                }
                return group.ToString();
            }

            // Если данных меньше или равно 116 бит – выводим всю строку с группировкой по 8 бит
            if (bits.Length <= bitsToDisplay * 2)
            {
                sb.Append(FormatBits(0, bits.Length));
            }
            else
            {
                sb.AppendLine("Первые 2*длина регистра бит:");
                sb.AppendLine(FormatBits(0, bitsToDisplay));
                sb.AppendLine();
                sb.AppendLine("Последние 2*длина регистра бит:");
                int start = bits.Length - bitsToDisplay;
                sb.AppendLine(FormatBits(start, bitsToDisplay));
            }
            return sb.ToString();
        }

    }
}
