namespace Pertamina.SolutionTemplate.Shared.Model;
public class Organization
{
#pragma warning disable IDE1006

    public string id { get; set; }
    public string name { get; set; }
    public string companyCode { get; set; }

#pragma warning restore IDE1006
}

public class Position
{
#pragma warning disable IDE1006

    public string id { get; set; }
    public string name { get; set; }
    public Organization organization { get; set; }
    public string kbo { get; set; }
    public object createdBy { get; set; }
    public string lastModified { get; set; }
    public bool isPublished { get; set; }
    public bool isHead { get; set; }
    public bool isOwner { get; set; }
    public bool isChief { get; set; }
    public bool isSync { get; set; }

#pragma warning restore IDE1006
}

public class MasterJsonIdamanUsers
{
#pragma warning disable IDE1006

    public object next { get; set; }
    public int page { get; set; }
    public int total { get; set; }
    public List<IdamanUsers> value { get; set; }

#pragma warning restore IDE1006
}

public class IdamanUsers
{
#pragma warning disable IDE1006

    public string id { get; set; }
    public string userId { get; set; }
    public Position position { get; set; }
    public string companyCode { get; set; }
    public string companyName { get; set; }
    public bool isActive { get; set; }
    public string country { get; set; }
    public string state { get; set; }
    public string city { get; set; }
    public string displayName { get; set; }
    public string employeeId { get; set; }
    public string lastName { get; set; }
    public string jobTitle { get; set; }
    public string email { get; set; }
    public string mobilePhone { get; set; }
    public string aboutMe { get; set; }
    public string username { get; set; }
    public string firstName { get; set; }
    public string address { get; set; }
    public string photo { get; set; }
    public object extensionAttributes { get; set; }
    public string idp { get; set; }
    public string directoryId { get; set; }
    public string lastModified { get; set; }
    public string employeeNumber { get; set; }
    public string employeeType { get; set; }
    public object cultureInfo { get; set; }
    public object language { get; set; }
    public object dateFormat { get; set; }
    public object timeFormat { get; set; }
    public bool isSync { get; set; }
    public object applicationParams { get; set; }
#pragma warning restore IDE1006
}


