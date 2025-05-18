namespace Homework2.STRUCT;

struct DecimalNumber
{
    private int number;

    // Конструктор
    public DecimalNumber(int number)
    {
        this.number = number;
    }

    // Метод для перевода в двоичную систему
    public string ToBinary()
    {
        return Convert.ToString(number, 2); // 2 — для бинарной системы
    }

    // Метод для перевода в восьмеричную систему
    public string ToOctal()
    {
        return Convert.ToString(number, 8); // 8 — для восьмеричной
    }

    // Метод для перевода в шестнадцатеричную систему
    public string ToHex()
    {
        return Convert.ToString(number, 16).ToUpper(); // 16 — для hex
    }

    // Метод для отображения числа
    public void Show()
    {
        Console.WriteLine($"Decimal: {number}");
        Console.WriteLine($"Binary: {ToBinary()}");
        Console.WriteLine($"Octal: {ToOctal()}");
        Console.WriteLine($"Hex: {ToHex()}");
    }
}