namespace GridPack.Units
{
   //Klasa reprezentująca upgrade jednostki. 
    public interface Buff
    {
        //Określa jak długo będzie trwać Bonus. Wartość ujemna oznacza nieskończoność. 
        int Duration{get; set;}

        //Opisuje sposób upgradu jednoski 
        void Apply(Unit unit);
        //Zwraca staty do początkowych wartości
        void Undo(Unit unit); 
        //Zwraca głęboką kopię obiektu Buff. 
        Buff Clone(); 
    }
}