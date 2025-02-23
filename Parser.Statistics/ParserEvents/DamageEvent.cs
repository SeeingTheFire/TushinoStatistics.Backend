namespace Parser.Statistics.ParserEvents;
[Flags]
public enum DamageEvent
{
    Event = 0,
    TimeSec = 1,
    KillerBotId = 2,
    KilledBotId = 3,
    Weapon = 4,
    Distance = 6,
    Damage = 7,
    Calibre = 9
}