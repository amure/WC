// It is the project entry. form CPAPA p.63 CN.ver
using System;
public class WordCountEntry
{
	static public void Main( string [] args)
	{
		Console.WriteLine("Beginning WordCount program ... ");

		//程序首先添加接受参数的功能，参见12页
		if (args.Length == 0 )
		{
			display_usage();//This function code is in P18.
			return;
		}
		
		WordCount theObj = new WordCount();
		theObj.processFile();

		Console.WriteLine( "Ending WordCount program ... ");
	}

	//copy from p18
	public static void display_usage()
	{
		string usage =
			@"usage: WordCount [-s] [-t] [-h] textfile.txt
			where [] indicates an optional argument
			-s prints a series of performance measurements
			-t prints a tracce of the program
			-h prints this message";
		Console.WriteLine( usage );
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
