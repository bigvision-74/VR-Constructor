[System.Serializable]
public class UserData
{
    public string userid;
    public string name;
    public string email;
    public string UserType;
    public string license;
    public string validity;
    public string startDate;
    public string endDate;
    public bool Status;
}

[System.Serializable]
public class UserStatus
{
    public bool error;
    public string messages;
    public int errorCode;
}

[System.Serializable]
public class LicenseData
{
    public bool activated;
}

[System.Serializable]
public class APIResponse
{
    public UserData data;
    public UserStatus status;
}

[System.Serializable]
public class APIResponseLicense
{
    public LicenseData licenseData;
    public UserStatus status;
}
