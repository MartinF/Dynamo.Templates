using System;
using System.Text;

// Escape ' - apostrophe?

namespace Dynamo.Template.Core
{
    public class SourceCompiler : ICompiler
    {
		// Constants
		public const Char TextDelimiter = '\'';
		public const Char ScriptOpenDelimiter = '{';
		public const Char ScriptCloseDelimiter = '}';
		public const Char ScriptPrintDelimeter = ':';
		public const Char JSVariableName = 't';

		// Fields
		private readonly StringBuilder _result = new StringBuilder();
	    private State _state = State.None;
		private readonly String _source;
	    private readonly int _sourceLength;
	    private int _currentIndex = 0;
		
		// Constructors
		public SourceCompiler(String source)
	    {
			if (source == null)
				throw new ArgumentNullException("source");

			_source = source;
		    _sourceLength = source.Length;

			// Create start - var t='';
			Append("var " + JSVariableName + "=" + TextDelimiter + TextDelimiter + ';');
	    }

		// Methods
		public virtual StringBuilder Compile()
		{
			for (; _currentIndex < _sourceLength; _currentIndex++)
			{
				if (IsCurrentAndNext(ScriptOpenDelimiter))
				{
					// Script Open

					MoveToNextChar();	// Because the next char is also ScriptOpenDelimeter
					
					// What kind of Script - Print or Block ?
					int newIndexPosition;
					if (IsFirstCharMet(ScriptPrintDelimeter, out newIndexPosition))
					{
						if (_state == State.Text)
						{
							AppendTextDelimiter();
							Append('+');
						}
						else
						{
							AppendVariableAssignmentOpen();
						}

						_state = State.ScriptPrint;
						_currentIndex = newIndexPosition;	// Move to the new index position				
					}
					else
					{
						if (_state == State.Text)
						{
							AppendTextAssignmentClose();
						}

						_state = State.ScriptBlock;			
					}
				}
				else if (IsCurrentAndNext(ScriptCloseDelimiter))
				{
					// Script Close

					MoveToNextChar();	// Because the next char is also ScriptCloseDelimeter

					if (_state == State.ScriptPrint)
					{
						AppendVariableAssignmentClose();
					}

					_state = State.None;
				}
				else if (Char.IsControl(CurrentChar()))	// IsCurrent('\n') || IsCurrent('\r') || IsCurrent('\t'))
				{
					// Skip
				}
				else
				{
					if (_state == State.None)
					{
						_state = State.Text;
						AppendTextAssignmentOpen();
					}

					AppendCurrent();
				}
			}

			if (_state == State.Text)
			{
				AppendTextAssignmentClose();
			}

			// Throw exception if State is Script-Print/Block and it was never closed?

			_state = State.None;

			return _result;
		}

		private void Append(Char c)
	    {
		    _result.Append(c);
	    }

	    private void Append(String str)
	    {
		    _result.Append(str);
	    }

	    private void AppendCurrent()
	    {
			Append(CurrentChar());
	    }

	    private void AppendVariableAssignmentOpen()
	    {
			Append(JSVariableName + "+=");		    
	    }

		private void AppendVariableAssignmentClose()
		{
			Append(';');
		}

	    private void AppendTextDelimiter()
	    {
			Append(TextDelimiter);
	    }
		
		private void AppendTextAssignmentOpen()
		{
			AppendVariableAssignmentOpen();
			AppendTextDelimiter();
	    }

	    private void AppendTextAssignmentClose()
	    {
		    AppendTextDelimiter();
		    AppendVariableAssignmentClose();
	    }

		/// <summary>
		/// Checks from the current index position and forward, if the first char met (which is not a separator), is of the char specified 
		/// </summary>
		/// <param name="charToLookFor">Char to search for</param>
		/// <param name="index">The index position of the char found</param>
		/// <returns>Whether the first char met (which is not a separator) is of the char specified</returns>
	    private Boolean IsFirstCharMet(Char charToLookFor, out int index)
	    {
			int i = _currentIndex + 1;	// Start index position

		    for (; i < _sourceLength; i++)
		    {
				var currentChar = _source[i];

				if (!Char.IsSeparator(currentChar))
				{
					// First Char which is not a separator found

					if (currentChar == charToLookFor)
					{
						index = i;
						return true;
					}

					break;
				}
		    }

			index = _currentIndex;
			return false;
	    }

	    private void MoveToNextChar()
	    {
		    _currentIndex++;
	    }

		private char CurrentChar()
		{
			return _source[_currentIndex];
		}

		private Boolean HasNext()
		{
			return _currentIndex + 1 < _sourceLength;
		}

		private Boolean IsNext(Char charToLookFor)
		{
			if (HasNext())
			{
				if (_source[_currentIndex + 1] == charToLookFor)
					return true;
			}

			return false;
		}

	    private Boolean IsCurrent(Char charToLookFor)
	    {
		    return CurrentChar() == charToLookFor;
	    }

		private Boolean IsCurrentAndNext(Char charToLookFor)
		{
			return IsCurrent(charToLookFor) && IsNext(charToLookFor);
		}

		private enum State
		{
			None,
			Text,
			ScriptBlock,
			ScriptPrint
		}
	}
}
