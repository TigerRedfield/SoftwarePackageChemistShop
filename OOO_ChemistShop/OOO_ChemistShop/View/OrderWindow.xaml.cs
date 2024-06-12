using OOO_ChemistShop.Classes;
using OOO_ChemistShop.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;

namespace OOO_ChemistShop.View
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        public List<Classes.ProductInOrder> userListOrder;
        //Объекты Word
        Word.Application wordApp;           //Приложение Word
        Word.Document wordDoc;          //Документ Word
        Word.Table wordTable;               //Таблица 
        Word.InlineShape wordShape;         //Рисунок
        Word.Paragraph wordPar;       //Абзацы документа и таблицы
        Word.Range wordRange;		//Текст абзаца и таблицы

        public OrderWindow()
        {
            InitializeComponent();
        }

        public OrderWindow(List<Classes.ProductInOrder> userListOrder)
        {
            InitializeComponent();
            this.userListOrder = userListOrder;
            this.DataContext = this;
            ShowInfo();
            cbPoint.ItemsSource = Helper.DB.Point.ToList();
            cbPoint.SelectedIndex = 0;
            if (Helper.User != null)
            {
                tbFIO.Text = Helper.User.UserFullName;
                tbFIO.IsEnabled = false;
            }
        }

        /// <summary>
        /// расчёты по заказу
        /// </summary>
        private void CalcOrder()
        {
            double summaTotal = 0;
            double summaWithDiscount = 0;
            double summaDiscount = 0;
            foreach(var item in userListOrder)
            {
                summaTotal += (double)item.ProductExtendedInOrder.Medicines.MedicineCost * item.countProductInOrder;
                summaWithDiscount += (double)item.ProductExtendedInOrder.ProductCostWithDiscount * item.countProductInOrder;
            }
            tbSummaTotal.Text = "Сумма заказа без скидки: " + summaTotal.ToString("f2");
            tbSummaWithDiscount.Text = summaWithDiscount.ToString("f2");
            summaDiscount = summaTotal - summaWithDiscount;
            tbSummaDiscount.Text = summaDiscount.ToString("f2");
        }

        /// <summary>
        /// отображать инфу о товаре
        /// </summary>
        private void ShowInfo()
        {
            ListBoxProductInOrder.ItemsSource = userListOrder;
            CalcOrder();
        }

        //Собственный метод убивания запущенных процессов
        /// <summary>
        /// Параметр – убиваемый процесс
        /// </summary>
        /// <param name="obj"></param>
        public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Не могу освободить объект " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }


        /// <summary>
        /// Событие для кнопки оформления заказа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (userListOrder.Count > 0) //есть товары
            {
                Model.Order order = new Model.Order();
                order.OrderClient = tbFIO.Text;

                var pointId = (cbPoint.SelectedItem as Model.Point).PointId;
                order.OrderPointId = pointId;
                order.DateOrder = DateTime.Now;
                order.OrderCode = new Random().Next(100, 1000);
                foreach (var item in userListOrder)
                {
                    Medicine medicines = Helper.DB.Medicine.Find(item.ProductExtendedInOrder.Medicines.MedicineId);

                    if (medicines.MedicineCount == 0) // если товар закончился - нельзя заказать 
                    {
                        sb.AppendLine("Товар " + item.ProductExtendedInOrder.Medicines.MedicineName + " сейчас отсутствует в продаже.");
                    }
                    if (medicines.MedicineCount < item.countProductInOrder && medicines.MedicineCount != 0) //нельзя заказать товаров больше, чем на складе
                    {
                        string MessageBoxCount = "";
                        MessageBoxCount += "Так как товара " + item.ProductExtendedInOrder.Medicines.MedicineName + " на складе сейчас: " + item.ProductExtendedInOrder.Medicines.MedicineCount + " штук, то мы не можем добавить больше в заказ";
                        sb.AppendLine(MessageBoxCount);
                    }
                    if(item.countProductInOrder == 0) // если пусто, то нельзя заказать
                    {
                        sb.AppendLine("Введите количество у товара " + item.ProductExtendedInOrder.Medicines.MedicineName);
                    }
                    if(sb.Length > 0) //если есть ошибки - уведомить
                    {
                        MessageBox.Show(sb.ToString());
                        return;
                    }
                    else
                    {
                        medicines.MedicineCount = medicines.MedicineCount - item.countProductInOrder; 
                        order.DateDelivery = DateTime.Now.AddDays(3);
                    }
                }

                order.OrderStatusId = 1;
                Helper.DB.Order.Add(order);

                try
                {
                    Helper.DB.SaveChanges();


                    //Создание сервера Word
                    try
                    {
                        wordApp = new Word.Application();
                        wordApp.Visible = false;
                    }
                    catch
                    {
                        MessageBox.Show("Товарный талон в Word создать не удалось");
                        return;
                    }

                    //Создание документа Word
                    wordDoc = wordApp.Documents.Add();      //Добавить новый пустой документ
                    wordDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait; // Книжная

                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    //Добавить логитип-картинку
                    wordShape = wordDoc.InlineShapes.AddPicture(App.pathExe + "/Logo/medicine.png",
                                                                               Type.Missing, Type.Missing, wordRange);

                    wordShape.Width = 80;
                    wordShape.Height = 80;
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;



                    //********Второй параграф - просто текст
                    wordRange.InsertParagraphAfter();
                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    wordPar.set_Style("Заголовок 1");               //Стиль, взятый из Word
                    wordRange.Font.Color = Word.WdColor.wdColorBlue;
                    wordRange.Font.Size = 20;
                    //Текст первого абзаца – заголовка документа
                    wordRange.Bold = 1;
                    wordRange.Text = "Талон заказа\n" + "№ " + order.OrderId.ToString();
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;


                    //********Третий параграф - просто текст
                    wordRange.InsertParagraphAfter();
                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    wordPar.set_Style("Заголовок 1");               //Стиль, взятый из Word
                    wordRange.Font.Color = Word.WdColor.wdColorBlue;
                    wordRange.Font.Size = 20;                                                        //Текст первого абзаца – заголовка документа
                    wordRange.Text = "\nДата оформления заказа: " + DateTime.Now.ToString("D", CultureInfo.GetCultureInfo("ru-RU")) + "\nФИО клиента, оформившего заказ: " + tbFIO.Text.ToString();
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;


                    //********Четвёртый параграф - просто текст
                    wordRange.InsertParagraphAfter();
                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    wordPar.set_Style("Заголовок 1");               //Стиль, взятый из Word
                    wordRange.Font.Color = Word.WdColor.wdColorBlue;
                    wordRange.Font.Size = 20;                                                      //Текст первого абзаца – заголовка документа
                    wordRange.Font.Italic = 1;
                    wordRange.Text = "Состав заказа:";
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;


                    //********Пятый параграф -  таблица
                    wordRange.InsertParagraphAfter();
                    wordRange = wordPar.Range;
                    wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wordRange.Font.Size = 16;
                    wordRange.Font.Name = "Arial";
                    wordTable = wordDoc.Tables.Add(wordRange, (this.Owner as CatalogWindow).productInOrders.Count + 1, 4);

                    wordTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    wordTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleDouble;
                    //Заголовков таблицы из ЭУ DataGrid
                    Word.Range cellRange;


                    cellRange = wordTable.Cell(1, 1).Range;
                    cellRange.Text = "Название";
                    cellRange = wordTable.Cell(1, 2).Range;
                    cellRange.Text = "Цена";
                    cellRange = wordTable.Cell(1, 3).Range;
                    cellRange.Text = "Количество";
                    cellRange = wordTable.Cell(1, 4).Range;
                    cellRange.Text = "Стоимость";


                    wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    wordPar.set_Style("Заголовок 2");               //Стиль, взятый из Word
                    for (int row = 2; row <= (this.Owner as CatalogWindow).productInOrders.Count + 1; row++)
                    {
                        cellRange = wordTable.Cell(row, 1).Range;
                        cellRange.Text = (this.Owner as CatalogWindow).productInOrders[row - 2].ProductExtendedInOrder.Medicines.MedicineName.ToString();
                        wordTable.Rows[row].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        wordRange.Font.Size = 18;
                        wordRange.Font.Color = Word.WdColor.wdColorBlack;
                        wordRange.Font.Name = "Times New Roman";
                        wordRange.Font.Italic = 0;

                        cellRange = wordTable.Cell(row, 2).Range;
                        cellRange.Text = (this.Owner as CatalogWindow).productInOrders[row - 2].ProductExtendedInOrder.Medicines.MedicineCost.ToString();
                        cellRange = wordTable.Cell(row, 3).Range;
                        cellRange.Text = (this.Owner as CatalogWindow).productInOrders[row - 2].countProductInOrder.ToString();
                        cellRange = wordTable.Cell(row, 4).Range;
                        cellRange.Text = ((this.Owner as CatalogWindow).productInOrders[row - 2].ProductExtendedInOrder.Medicines.MedicineCost * (this.Owner as CatalogWindow).productInOrders[row - 2].countProductInOrder).ToString();
                    }


                    //********Шестой параграф - просто текст
                    wordRange.InsertParagraphAfter();
                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    wordPar.set_Style("Заголовок 1");               //Стиль, взятый из Word
                    wordRange.Font.Color = Word.WdColor.wdColorOrange;
                    wordRange.Font.Size = 20;
                    wordRange.Bold = 3;
                    wordRange.Text = "Итого:";
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    //********Седьмой параграф - просто текст
                    wordRange.InsertParagraphAfter();
                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    wordPar.set_Style("Заголовок 1");               //Стиль, взятый из Word
                    wordRange.Font.Color = Word.WdColor.wdColorDarkGreen;
                    wordRange.Font.Size = 20;
                    wordRange.Bold = 3;
                    wordRange.Text = tbSummaTotal.Text.ToString() + " рублей";
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    //********Восьмой параграф - просто текст
                    wordRange.InsertParagraphAfter();
                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    wordPar.set_Style("Заголовок 1");               //Стиль, взятый из Word
                    wordRange.Font.Color = Word.WdColor.wdColorDarkGreen;
                    wordRange.Font.Size = 20;
                    wordRange.Bold = 3;
                    wordRange.Text = "Величина скидки: " + tbSummaDiscount.Text.ToString() + " рублей";
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    //********Девятый параграф - просто текст
                    wordRange.InsertParagraphAfter();
                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    wordPar.set_Style("Заголовок 1");               //Стиль, взятый из Word
                    wordRange.Font.Color = Word.WdColor.wdColorDarkGreen;
                    wordRange.Font.Size = 20;
                    wordRange.Bold = 3;
                    wordRange.Text = "Сумма заказа со скидкой: " + tbSummaWithDiscount.Text.ToString() + " рублей";
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                    //*************Десятый параграф - печать
                    wordRange.InsertParagraphAfter();
                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    wordPar.set_Style("Заголовок 1");               //Стиль, взятый из Word
                    wordRange.Font.Color = Word.WdColor.wdColorBlue;
                    wordRange.Font.Size = 33;
                    wordRange.Bold = 4;
                    wordRange.Text = "@";
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

                    //*************Одинадцатый параграф - подпись
                    wordRange.InsertParagraphAfter();
                    wordPar = wordDoc.Paragraphs.Add();
                    wordRange = wordPar.Range;
                    wordPar.set_Style("Заголовок 1");               //Стиль, взятый из Word
                    wordRange.Font.Color = Word.WdColor.wdColorBlue;
                    wordRange.Font.Size = 20;
                    wordRange.Text = "Аптека Медфарм ©️";
                    wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    //Можно выполнить заливку заголовка таблицы
                    wordTable.Rows[1].Shading.ForegroundPatternColor = Word.WdColor.wdColorYellow;
                    wordTable.Rows[1].Shading.BackgroundPatternColorIndex = Word.WdColorIndex.wdBlue;
                    wordTable.Rows[1].Range.Bold = 1;
                    wordTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wordRange.Font.Size = 20;
                    wordRange.Font.Color = Word.WdColor.wdColorBlue;
                    wordRange.Font.Name = "Times New Roman";
                    wordRange.Font.Italic = 1;

                    wordApp.Visible = true;
                    //Сохранение документа
                    string fileName = App.pathExe + @"\Талон " + "№" + order.OrderId.ToString() + " (" + DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss") + ")";
                    wordDoc.SaveAs(fileName + ".docx");
                    wordDoc.SaveAs(fileName + ".pdf", Word.WdExportFormat.wdExportFormatPDF);
                    //Завершение работы с Word
                    wordDoc.Close(true, null, null);                //Сначала закрыть документ
                    wordApp.Quit();                     //Выход из Word
                                                        //Вызвать свою подпрограмму убивания процессов
                    releaseObject(wordPar);                 //Уничтожить абзац
                    releaseObject(wordDoc);                 //Уничтожить документ
                    releaseObject(wordApp);             //Удалить из Диспетчера задач

                    foreach (var item in userListOrder)
                    {
                        Model.MedicineOrder orderMedicine = new Model.MedicineOrder();
                        orderMedicine.OrderId = order.OrderId;
                        orderMedicine.MedicineId = item.ProductExtendedInOrder.Medicines.MedicineId;
                        orderMedicine.ProductCount = item.countProductInOrder;
                        Helper.DB.MedicineOrder.Add(orderMedicine);
                        (this.Owner as CatalogWindow).ListBoxProduct.Items.Refresh();
                        Helper.DB.SaveChanges();
                    }
                    this.Close();
                    userListOrder.Clear();
                    MessageBox.Show("Заказ оформлен");
                }
                catch
                {
                    MessageBox.Show("Заказ не удался");
                }
            }
            else
            {
                MessageBox.Show("Сохранять нечего");
            }
        }
        

        /// <summary>
        /// Событие для кнопки удаление товара из окна заказа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as ProductInOrder;
            userListOrder.Remove(item);
            ListBoxProductInOrder.Items.Refresh();
            ShowInfo();
        }
        /// <summary>
        /// Событие для изменения количества товара в заказе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCountProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            var item = (sender as TextBox).DataContext as ProductInOrder;
            var repIndex = userListOrder.IndexOf(item);
            
            if((sender as TextBox).Text == Convert.ToString(0))
            {
                (sender as TextBox).Text = Convert.ToInt32(1) + "";
            }
            else
            {
                try
                {
                    item.countProductInOrder = int.Parse((sender as TextBox).Text);
                }
                catch
                {
                    item.countProductInOrder = 0;
                }
            }
            userListOrder[repIndex] = item;
            ShowInfo();
        }
        /// <summary>
        /// Событие кнопки назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            //Предупреждение о том, что пользователь вернётся в предыдущее окно
            if (MessageBox.Show("Вы точно хотите вернуться на экран выбора товаров?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close(); //Закрытие окна
            }
        }

        /// <summary>
        /// Разрешение на ввод только цифр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCountProduct_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
                if (!char.IsDigit(e.Text, 0))
                    e.Handled = true;
        }

        /// <summary>
        /// Запрет на ввод пробелов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCountProduct_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
