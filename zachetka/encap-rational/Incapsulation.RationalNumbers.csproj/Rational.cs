/ Модель представления, предназначенная для управления входом 
// Выделяем методы в интерфейс
internal interface ILastUsernameProvider
{ 
    string ReadLastUserName(); 
    void SaveLastUserName(string userName);
}
 
internal class LastUsernameProvider : ILastUsernameProvider
{ 
    // Читаем имя последнего пользователя из некоторого источника данных
    public string ReadLastUserName() { return "Jonh Doe"; } 
    // Сохраняем это имя, откуда его можно будет впоследствии прочитать
    public void SaveLastUserName(string userName) { }
}
 
public class LoginViewModel
{ 
    private readonly ILastUsernameProvider _provider; 
    
    // Единственный открытый конструктор создает реальный провайдер
    public LoginViewModel() 
        : this(new LastUsernameProvider()) 
    {} 
    
    // "Внутренний" предназначен только для тестирования и может принимать "фейк"
    internal LoginViewModel(ILastUsernameProvider provider) 
    { 
        _provider = provider; 
        UserName = _provider.ReadLastUserName(); 
    } 

    
    public string UserName { get; set; } 
    
    public void Login() 
    { 
        _provider.SaveLastUserName(UserName); 
    }
}