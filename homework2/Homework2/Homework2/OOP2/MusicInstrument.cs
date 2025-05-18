namespace Homework2.OOP2;

public abstract class MusicInstrument
{
    public string Name { get; protected set; }

    public abstract void Sound();
    public abstract void Show();
    public abstract void Desc();
    public abstract void History();
}

public class Violin : MusicInstrument
{
    public Violin()
    {
        this.Name = "Violin";
    }

    public override void Sound()
    {
        Console.WriteLine("Violin makes a smooth and high-pitched sound.");
    }

    public override void Show()
    {
        Console.WriteLine($"Instrument: {Name}");
    }

    public override void Desc()
    {
        Console.WriteLine("The violin is a string instrument with four strings tuned in perfect fifths.");
    }

    public override void History()
    {
        Console.WriteLine("The modern violin was developed in the 16th century in Italy.");
    }
}

public class Trombone : MusicInstrument
{
    public Trombone()
    {
        this.Name = "Trombone";
    }

    public override void Sound()
    {
        Console.WriteLine("Trombone produces a deep and rich brassy sound.");
    }

    public override void Show()
    {
        Console.WriteLine($"Instrument: {Name}");
    }

    public override void Desc()
    {
        Console.WriteLine("The trombone is a brass instrument with a telescoping slide.");
    }

    public override void History()
    {
        Console.WriteLine("The trombone dates back to the 15th century in Europe.");
    }
}

public class Ukulele : MusicInstrument
{
    public Ukulele()
    {
        this.Name = "Ukulele";
    }

    public override void Sound()
    {
        Console.WriteLine("Ukulele produces a light, cheerful, and melodic sound.");
    }

    public override void Show()
    {
        Console.WriteLine($"Instrument: {Name}");
    }

    public override void Desc()
    {
        Console.WriteLine("The ukulele is a small four-stringed instrument resembling a small guitar.");
    }

    public override void History()
    {
        Console.WriteLine("The ukulele originated in Hawaii in the 19th century, inspired by Portuguese instruments.");
    }
}

public class Cello : MusicInstrument
{
    public Cello()
    {
        this.Name = "Cello";
    }

    public override void Sound()
    {
        Console.WriteLine("The cello produces a deep, warm, and expressive sound.");
    }

    public override void Show()
    {
        Console.WriteLine($"Instrument: {Name}");
    }

    public override void Desc()
    {
        Console.WriteLine("The cello is a bowed string instrument with four strings, larger than a violin but smaller than a double bass.");
    }

    public override void History()
    {
        Console.WriteLine("The cello was developed in the 16th century as part of the violin family and gained prominence in the Baroque era.");
    }
}

