﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Observers
{
	public class StackOperationsLogger
	{
	    public StringBuilder Log = new StringBuilder();

        public void SubscribeOn<T>(ObservableStack<T> stack)
        {
            stack.Pushed += (s) => { Log.Append($"+{s.Value}"); };
            stack.Poped += (s) => { Log.Append($"-{s.Value}"); };
		}

	    public string GetLog()
		{
			return Log.ToString();
		}
	}

	public class ObservableStack<T> 
	{
	    public event Action<StackEventData<T>> Pushed;
	    public event Action<StackEventData<T>> Poped;

		List<T> data = new List<T>();

		public void Push(T obj)
		{
			data.Add(obj);
		    Pushed?.Invoke(new StackEventData<T> { IsPushed = true, Value = obj });
		}

		public T Pop()
		{
			if (data.Count == 0)
				throw new InvalidOperationException();
			var result = data[data.Count - 1];
		    Poped?.Invoke(new StackEventData<T> { IsPushed = true, Value = result });

		    return result;
		}
	}
}
