namespace ExpenSpend.Domain.DTOs.Emails;

public class EmailConfigurationDto
{
    public string From { get; set; } = null!;
    public string SmtpServer { get; set; } = null!;
    public int Port { get; set; }
    public string UserName { get; set; } = null!;
    public string UserPassword { get; set; } = null!;
}
