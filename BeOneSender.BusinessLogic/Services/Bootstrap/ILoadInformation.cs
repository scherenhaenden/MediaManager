namespace BeOneSender.BusinessLogic.Services.Bootstrap;

public interface ILoadInformation
{
    // get the json

    // filter the information
    // load the information

    Task LoadData();

    Task LoadData(string path);
}