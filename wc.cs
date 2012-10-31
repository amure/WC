// It is the project entry. form CPAPA p.63 CN.ver
using System;
public class WordCountEntry
{
	static public void Main()
	{
		Console.WriteLine("Beginning WordCount program ... ");

		WordCount theObj = new WordCount();
		theObj.processFile();

		Console.WriteLine( "Ending WordCount program ... ");
	}
}

public class WordCount
{
	public void processFile()
	{
		openFiles();
		readFiles();
		countWords();
		writeWords();
	}

	private void countWords()
	{
		Console.WriteLine( "!!! WordCount.countWords() " );
	}
	
		
	private void readFiles()
	{
		Console.WriteLine( "!!! WordCount.readFiles() " );
	}
	
	private void openFiles()
	{
		Console.WriteLine( "!!! WordCount.openFiles() " );
	}
	
	private void writeWords()
	{
		Console.WriteLine( "!!! WordCount.writeWords() " );
	}
}
