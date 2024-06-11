using Drive2;
using System.Runtime.CompilerServices;
using System.Timers;

class Program
{
    public static Render render = new Render();

    static void Main()
    {
        render.GenerateAndRenderFirstFrame();
        bool crashed = false;
        int score = 0;

        while(!crashed){
            Thread.Sleep(100);
            score++;

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.LeftArrow)
                {
                    render.MoveShipLeft();                    
                }
                else if(key.Key == ConsoleKey.RightArrow)
                {
                    render.MoveShipRight();
                }

            }
        render.UpdateFrame();
        crashed = render.RenderFrame();
        }
        Console.WriteLine("You dead");
        Console.WriteLine($"Score: {score}");
        Console.ReadLine();
    }
}


