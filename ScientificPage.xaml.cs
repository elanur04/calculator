namespace calculator;
public partial class ScientificPage : ContentPage
{
    private string currentOperator = "";
    private double? firstOperand = null;
    private bool isAwaitingSecondOperand = false;
    private bool isDegreesMode = true;
    private bool isSecondFunctionActive = false;
    private bool isHyperbolicActive = false;
    private bool isNewEntry = true;
    public ScientificPage()
    {
        InitializeComponent();
    }
    private void NumClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        string digit = button.Text;


        if (cScreen.Text == "0" || isNewEntry)
        {
            cScreen.Text = digit;
            // Bayraðý false yapýyoruz, çünkü artýk sayýnýn devamý gelebilir (örneðin 8'dan sonra 3'e basarak 83 yazmak gibi)-> bu hatayý aldýðým için belirtmek istedim.
            isNewEntry = false;
        }
        else
        {
            cScreen.Text += digit;
        }
    }

    private void OperatorClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        currentOperator = button.Text;
        if (double.TryParse(cScreen.Text, out double number))
        {
            firstOperand = number;
        }

        // NumClicked metodunun beklediði bayraðý 'true' yaptým.
        isNewEntry = true;
        lblHistory.Text = $"{cScreen.Text} {currentOperator}";
    }

    private void Clear_Clicked(object sender, EventArgs e)
    {
        cScreen.Text = "0";
        lblHistory.Text = "";

    }
    double number1 = 0;

    private void Square_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        lblHistory.Text = cScreen.Text + " " + btn.Text;

        number1 = double.Parse(cScreen.Text);
        var result = number1 * number1;
        cScreen.Text = result.ToString();
    }
    private void BackSpaceClicked(object sender, EventArgs e)
    {
        cScreen.Text = cScreen.Text.Length > 1 ? cScreen.Text[..^1] : "0";
    }
    // 1/x için
    private void Reciprocal_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(cScreen.Text, out double number))
        {
            if (number == 0)
            {
                cScreen.Text = "Sýfýra bölünemez";
                return;
            }
            double result = 1 / number;
            cScreen.Text = result.ToString();
        }
    }
    // karekök için
    private void SquareRoot_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(cScreen.Text, out double number))
        {
            if (number < 0)
            {
                cScreen.Text = "Geçersiz giriþ";
                return;
            }
            double result = Math.Sqrt(number);
            cScreen.Text = result.ToString();
        }
    }
    // +/-
    private void PlusMinus_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(cScreen.Text, out double number))
        {
            cScreen.Text = (-number).ToString();
        }
    }
    // , için
    private void Decimal_Clicked(object sender, EventArgs e)
    {
        if (!cScreen.Text.Contains(","))
        {
            cScreen.Text += ",";
        }
    }
    private void Pi_Clicked(object sender, EventArgs e)
    {
        cScreen.Text = Math.PI.ToString();
    }

    // Euler sayýsý 
    private void E_Clicked(object sender, EventArgs e)
    {
        cScreen.Text = Math.E.ToString();
    }
    // mutlak deðer
    private void Absolute_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(cScreen.Text, out double number))
        {
            cScreen.Text = Math.Abs(number).ToString();
        }
    }
    private void Factorial_Clicked(object sender, EventArgs e)
    {
        // Sayý tamsayý ve pozitif olmalý -> hata kontrolü

        if (int.TryParse(cScreen.Text, out int number) && number >= 0)
        {

            double result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            cScreen.Text = result.ToString();
        }
        else
        {
            cScreen.Text = "Geçersiz giriþ";
        }
    }
    // 10^x
    private void TenPowerX_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(cScreen.Text, out double number))
        {
            cScreen.Text = Math.Pow(10, number).ToString();
        }
    }

    private void Log_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(cScreen.Text, out double number) && number > 0)
        {
            cScreen.Text = Math.Log10(number).ToString();
        }
        else
        {
            cScreen.Text = "Geçersiz giriþ";
        }
    }

    private void Ln_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(cScreen.Text, out double number) && number > 0)
        {
            cScreen.Text = Math.Log(number).ToString();
        }
        else
        {
            cScreen.Text = "Geçersiz giriþ";
        }
    }
    private void EqualClicked(object sender, EventArgs e)
    {
        if (firstOperand.HasValue && !string.IsNullOrEmpty(currentOperator) && double.TryParse(cScreen.Text, out double secondOperand))
        {
            lblHistory.Text += $"{cScreen.Text} =";

            double result = Calculate(firstOperand.Value, secondOperand, currentOperator);

            if (double.IsInfinity(result) || double.IsNaN(result))
            {
                cScreen.Text = "Hata";
            }
            else
            {
                cScreen.Text = result.ToString();
            }

            firstOperand = null;

            isAwaitingSecondOperand = false;
        }
    }
    private double Calculate(double first, double second, string op)
    {
        switch (op)
        {
            case "+": return first + second;
            case "-": return first - second;
            case "*": return first * second;
            case "/":
                if (second == 0) return double.NaN;
                return first / second;
            case "mod": return first % second;
            case "x^y": return Math.Pow(first, second);
            case "exp": return first * Math.Pow(10, second);
            default: return 0;
        }
    }
    private void ToggleTrigonometryMenu_Clicked(object sender, EventArgs e)
    {
     
        TrigonometryGrid.IsVisible = !TrigonometryGrid.IsVisible;
    }
    private void SecondFunction_Clicked(object sender, EventArgs e)
    {
        isSecondFunctionActive = !isSecondFunctionActive;
        BtnSecond.BackgroundColor = isSecondFunctionActive ? Colors.CornflowerBlue : Colors.LightGray;
    }

    // sin, cos, tan,sec, csc,cot butonlarýnýn hepsi bu metodu kullanýr

    private void TrigFunction_Clicked(object sender, EventArgs e)
    {
        if (!double.TryParse(cScreen.Text.Replace(',', '.'), out double number))
        {
            cScreen.Text = "Geçersiz giriþ";
            return;
        }

        double angleInRadians = isDegreesMode ? (number * Math.PI) / 180.0 : number;
        double result = 0;
        string function = (sender as Button).Text;

        switch (function)
        {
            case "sin":
                if (isHyperbolicActive)
                    result = isSecondFunctionActive ? Math.Asinh(number) : Math.Sinh(number);
                else
                    result = isSecondFunctionActive ? Math.Asin(angleInRadians) : Math.Sin(angleInRadians);
                break;
            case "cos":
                if (isHyperbolicActive)
                    result = isSecondFunctionActive ? Math.Acosh(number) : Math.Cosh(number);
                else
                    result = isSecondFunctionActive ? Math.Acos(angleInRadians) : Math.Cos(angleInRadians);
                break;
            case "tan":
                if (isHyperbolicActive)
                    result = isSecondFunctionActive ? Math.Atanh(number) : Math.Tanh(number);
                else
                    result = isSecondFunctionActive ? Math.Atan(angleInRadians) : Math.Tan(angleInRadians);
                break;
            case "sec": result = 1 / Math.Cos(angleInRadians); break;
            case "csc": result = 1 / Math.Sin(angleInRadians); break;
            case "cot": result = 1 / Math.Tan(angleInRadians); break;
        }

        if (isSecondFunctionActive && !isHyperbolicActive && isDegreesMode)
        {
            result = (result * 180.0) / Math.PI;
        }

        lblHistory.Text = $"{function}({cScreen.Text})";
        cScreen.Text = result.ToString();
      
    }
}