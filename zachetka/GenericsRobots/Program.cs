using System.Collections.Generic;
using System.Linq;
using NUnitLite;
using System;

class Program
{
	static void Main(string[] args)
	{
        
		new AutoRun().Execute(args);
	}
}

class Account
{
    public virtual void DoTransfer(int sum)
    {
        Console.WriteLine($"������ ������� �� ���� {sum} ��������");
    }
}
class DepositAccount : Account
{
    public override void DoTransfer(int sum)
    {
        Console.WriteLine($"������ ������� �� ���������� ���� {sum} ��������");
    }
}

interface IBank<out T>
{
    T CreateAccount(int sum);
}

class Bank<T> : IBank<T> where T : Account, new()
{
    public T CreateAccount(int sum)
    {
        T acc = new T();  // ������� ����
        acc.DoTransfer(sum);
        return acc;
    }
}
