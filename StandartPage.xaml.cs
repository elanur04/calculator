namespace calculator;

public partial class StandartPage : ContentPage
{
	public StandartPage()
	{
		InitializeComponent();
	}

    private void NumClicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;

        var str = cScreen.Text + btn.Text;
        double.TryParse(str, out double value);
        cScreen.Text = value.ToString();
    }

    double number1 = 0;
    string operatorSymbol = "";
    private void OperatorClicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        lblHistory.Text = cScreen.Text + " " + btn.Text;
        number1 = double.Parse(cScreen.Text);
        cScreen.Text = "0";
        operatorSymbol = btn.Text;
    }

    private void EqualClicked(object sender, EventArgs e)
    {
        double number2 = double.Parse(cScreen.Text);
        double result = 0;
        switch (operatorSymbol)
        {
            case "+":
                result = (number1 + number2);
                break;
            case "-":
                result = (number1 - number2);
                break;
            case "*":
                result = (number1 * number2);
                break;
            case "/":
                result = (number1 / number2);
                break;
        }

        cScreen.Text = result.ToString();
        lblHistory.Text = $"{number1} {operatorSymbol} {number2}";
        number1 = result;
    }

    private void Clear_Clicked(object sender, EventArgs e)
    {
        cScreen.Text = "0";
        lblHistory.Text = "";

    }

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

    private void Percentage_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(cScreen.Text, out double number))
        {
            cScreen.Text = (number / 100).ToString();
        }
    }
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
    private void CE_Clicked(object sender, EventArgs e)
    {
        cScreen.Text = "0";
    }
    private void PlusMinus_Clicked(object sender, EventArgs e)
    {
        if (double.TryParse(cScreen.Text, out double number))
        {
            cScreen.Text = (-number).ToString();
        }
    }
    private void Decimal_Clicked(object sender, EventArgs e)
    {
        if (!cScreen.Text.Contains(","))
        {
            cScreen.Text += ",";
        }
    }
}












