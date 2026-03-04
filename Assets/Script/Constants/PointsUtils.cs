public class PointsUtils
{
    public const int Coin = 200;
    public const int WallBlock = 50;
    public const int PowerUp = 1000;

    //Combo
    public const int Combo8 = 6000;
    public const int Combo7 = 4000;
    public const int Combo6 = 2000;
    public const int Combo5 = 1000;
    public const int Combo4 = 800;
    public const int Combo3 = 400;
    public const int Combo2 = 200;
    public const int Combo1 = 100;

    public static int GetPointByCombo(int combo)
    {
        switch(combo)
        {
            case 1:
                return Combo1;
            case 2:
                return Combo2;
            case 3:
                return Combo3;
            case 4:
                return Combo4;
            case 5:
                return Combo5;
            case 6:
                return Combo6;
            case 7:
                return Combo7;
            case 8:
                return Combo8;
            default :
                return 0;
        }
    }

    //Meta
    public const int Meta = 5000;

    //Time
    public const int SecondPoint = 50;
}
