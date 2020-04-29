using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Elevator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestElevator testElevator = null;
        private TestElevator testElevator1 = new FirstElevator();
        private TestElevator testElevator2 = new SecondElevator();
        private TestElevator testElevator3 = new ThirdElevator(); 
        private TestElevator testElevator4 = new FouthElevator();

        private List<int> list = new List<int>();
        public List<int> storeList = new List<int>();

        public int flag = 0;

        public MainWindow()
        {
            InitializeComponent();
            chooseFloor.IsEnabled = false;
            getOnElevator.IsEnabled = false;
            workButton.IsEnabled = false;
            testElevator1.mode = testElevator1.WAIT_MODE;
            testElevator2.mode = testElevator1.WAIT_MODE;
            testElevator3.mode = testElevator1.WAIT_MODE;
            testElevator4.mode = testElevator1.WAIT_MODE;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            switch(button.Name)
            {
                case "btn0": btn0.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(0); break;
                case "btn1": btn1.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(1); break;
                case "btn2": btn2.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(2); break;
                case "btn3": btn3.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(3); break;
                case "btn4": btn4.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(4); break;
                case "btn5": btn5.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(5); break;
                case "btn6": btn6.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(6); break;
                case "btn7": btn7.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(7); break;
                case "btn8": btn8.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(8); break;
                case "btn9": btn9.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(9); break;
                case "btn10": btn10.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(10); break;
                case "btn11": btn11.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(11); break;
                case "btn12": btn12.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(12); break;
                case "btn13": btn13.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(13); break;
                case "btn14": btn14.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(14); break;
                case "btn15": btn15.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(15); break;
                case "btn16": btn16.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(16); break;
                case "btn17": btn17.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(17); break;
                case "btn18": btn18.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(18); break;
                case "btn19": btn19.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(19); break;
                case "btn20": btn20.Background = new SolidColorBrush(Colors.GreenYellow); list.Add(20); break;
            }
            getOnElevator.IsEnabled = true;
            workButton.IsEnabled = true;
        }

        private void IsCheck(object sender, RoutedEventArgs e)
        {
            var radioCheck = sender as System.Windows.Controls.RadioButton;
            switch (radioCheck.Name)
            {
              case "_1st": testElevator = testElevator1; flag = 1; Remake(); break;
              case "_2nd": testElevator = testElevator2; flag = 2; Remake(); break;
              case "_3rd": testElevator = testElevator3; flag = 3; Remake(); break;
              case "_4th": testElevator = testElevator4; flag = 4; Remake(); break;
            }
            getOnElevator.IsEnabled = true;
        }

        private void ChoosePosition(object sender, RoutedEventArgs e)
        {
            var choosemode=sender as System.Windows.Controls.Button;
            if (Floor.Text=="20")
            {
                up.IsEnabled = false;
                down.IsEnabled = true;
            }
            else if (Floor.Text=="0")
            {
                up.IsEnabled = true;
                down.IsEnabled = false;
            }
            else
            {
                up.IsEnabled = true;
                down.IsEnabled = true;
            }
        }

        private void ChooseMode(object sender, RoutedEventArgs e)
        {
            if (flag==2&&int.Parse(Floor.Text)%2==0)
            {
                return;
            }
            else if (flag==3 && int.Parse(Floor.Text) % 2 == 0&& int.Parse(Floor.Text) != 1)
            {
                return;
            }
           
            var button = sender as System.Windows.Controls.Button;
            string floor = Floor.Text;
            if (button.Name=="↑"&&testElevator.mode==testElevator.DOWN_MODE)
            {
                storeList.Add(int.Parse(floor));
            }
            if (button.Name == "↓" && testElevator.mode == testElevator.UP_MODE)
            {
                storeList.Add(int.Parse(floor));
            }
            list.Add(int.Parse(floor));
            workButton.IsEnabled = true;
        
            if (thisFloor.Text==floor)
            {
                getOnElevator.IsEnabled = false;
                chooseFloor.IsEnabled = true; 
                if (flag == 2)
                {
                    btn0.IsEnabled = false;
                    btn2.IsEnabled = false;
                    btn4.IsEnabled = false;
                    btn6.IsEnabled = false;
                    btn8.IsEnabled = false;
                    btn10.IsEnabled = false;
                    btn12.IsEnabled = false;
                    btn14.IsEnabled = false;
                    btn16.IsEnabled = false;
                    btn18.IsEnabled = false;
                    btn20.IsEnabled = false;
                }
                else if (flag == 3)
                {
                    btn3.IsEnabled = false;
                    btn5.IsEnabled = false;
                    btn7.IsEnabled = false;
                    btn9.IsEnabled = false;
                    btn11.IsEnabled = false;
                    btn13.IsEnabled = false;
                    btn15.IsEnabled = false;
                    btn17.IsEnabled = false;
                    btn19.IsEnabled = false;
                }
            }
            else
            {
                Log.Text = "please wait";
            }
           
        }

        private void GoOn(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(customer.Text);
            
            
            if ((number+testElevator.customerNum)>testElevator.MAX_SIZE)
            {
                Log.Text = "More People!!";
                return;
            }
            else if (testElevator.customerNum*60>testElevator.MAX_WEIGHT)
            {
                Log.Text = "Overweight!!";
                return;
            }
            testElevator.customerNum += int.Parse(customer.Text);
            testElevator.weight += testElevator.customerNum * 60;

            testElevator.waitFloor = thisFloor.Text;
            string target = testElevator.Working(list,testElevator,storeList);
            if (list.Count!=0)
            {
                list.Remove(list[0]);
            }
            if (list.Count==0||int.Parse(testElevator.waitFloor)==list[0])
            {
                
                testElevator.mode = testElevator.WAIT_MODE;
                workButton.IsEnabled = false;
            }
            testElevator.waitFloor = target;
            thisFloor.Text = target;
            switch (target)
            {
                case "0": btn0.Background = new SolidColorBrush(Colors.LightGray); break;
                case "1": btn1.Background = new SolidColorBrush(Colors.LightGray); break;
                case "2": btn2.Background = new SolidColorBrush(Colors.LightGray); break;
                case "3": btn3.Background = new SolidColorBrush(Colors.LightGray); break;
                case "4": btn4.Background = new SolidColorBrush(Colors.LightGray); break;
                case "5": btn5.Background = new SolidColorBrush(Colors.LightGray); break;
                case "6": btn6.Background = new SolidColorBrush(Colors.LightGray); break;
                case "7": btn7.Background = new SolidColorBrush(Colors.LightGray); break;
                case "8": btn8.Background = new SolidColorBrush(Colors.LightGray); break;
                case "9": btn9.Background = new SolidColorBrush(Colors.LightGray); break;
                case "10": btn10.Background = new SolidColorBrush(Colors.LightGray); break;
                case "11": btn11.Background = new SolidColorBrush(Colors.LightGray); break;
                case "12": btn12.Background = new SolidColorBrush(Colors.LightGray); break;
                case "13": btn13.Background = new SolidColorBrush(Colors.LightGray); break;
                case "14": btn14.Background = new SolidColorBrush(Colors.LightGray); break;
                case "15": btn15.Background = new SolidColorBrush(Colors.LightGray); break;
                case "16": btn16.Background = new SolidColorBrush(Colors.LightGray); break;
                case "17": btn17.Background = new SolidColorBrush(Colors.LightGray); break;
                case "18": btn18.Background = new SolidColorBrush(Colors.LightGray); break;
                case "19": btn19.Background = new SolidColorBrush(Colors.LightGray); break;
                case "20": btn20.Background = new SolidColorBrush(Colors.LightGray); break;
            }
        }

        private void Remake()
        {
            chooseFloor.IsEnabled = false;
            getOnElevator.IsEnabled = false;
            workButton.IsEnabled = false;
            list.Clear();
            btn0.Background = new SolidColorBrush(Colors.LightGray);
            btn1.Background = new SolidColorBrush(Colors.LightGray);
            btn2.Background = new SolidColorBrush(Colors.LightGray);
            btn3.Background = new SolidColorBrush(Colors.LightGray);
            btn4.Background = new SolidColorBrush(Colors.LightGray);
            btn5.Background = new SolidColorBrush(Colors.LightGray);
            btn6.Background = new SolidColorBrush(Colors.LightGray);
            btn7.Background = new SolidColorBrush(Colors.LightGray);
            btn8.Background = new SolidColorBrush(Colors.LightGray);
            btn9.Background = new SolidColorBrush(Colors.LightGray);
            btn10.Background = new SolidColorBrush(Colors.LightGray);
            btn11.Background = new SolidColorBrush(Colors.LightGray);
            btn12.Background = new SolidColorBrush(Colors.LightGray);
            btn13.Background = new SolidColorBrush(Colors.LightGray);
            btn14.Background = new SolidColorBrush(Colors.LightGray);
            btn15.Background = new SolidColorBrush(Colors.LightGray);
            btn16.Background = new SolidColorBrush(Colors.LightGray);
            btn17.Background = new SolidColorBrush(Colors.LightGray);
            btn18.Background = new SolidColorBrush(Colors.LightGray);
            btn19.Background = new SolidColorBrush(Colors.LightGray);
            btn20.Background = new SolidColorBrush(Colors.LightGray);
        }
    }
}
