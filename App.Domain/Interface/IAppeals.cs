namespace App.Domain.Interface;

public interface IAppeals
{
    List<Appeal> GetAppeals();
    void AddAppeal(Appeal appeal);
    void SortDeadlineDate();
    void DeleteAppeal(Guid appealId); // Accept a Guid instead of an int

}