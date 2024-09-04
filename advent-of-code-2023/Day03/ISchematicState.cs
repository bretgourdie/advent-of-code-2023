namespace advent_of_code_2023.Day03;
internal interface ISchematicState
{
    ISchematicState Handle(string[] schematic, int x, int y);
    long GetSum();
}
