// It is the project entry. form CPAPA p.63 CN.ver
using System;
public class WordCountEntry
{
	static public void Main( string [] args)
	{
		Console.WriteLine("Beginning WordCount program ... ");

		//����������ӽ��ܲ����Ĺ��ܣ��μ�12ҳ
		if (args.Length == 0 )
		{
			display_usage();//This function code is in P18.
			return;
		}else{	//There is a code block about foreach in P14
			bool traceOn = false;
			bool spyOn = false;

			foreach ( string option in args )
			{
				if ( option.Equals ( "-t" ))
					traceOn = true;
				else if (option.Equals ( "-s" ))
					spyOn = true;
				else if (option.Equals ( "-h" ))
				{display_usage(); return;}
				else
					Console.WriteLine( @"check_valid_file_type( option );" );
			}
		//} this is error if there is a }
		//i want to read traceOn & spyON
		if ( traceOn ) 
			Console.WriteLine( "traceOn = true" );
		else
			Console.WriteLine( "traceOn = false" );
		if ( spyOn ) 
			Console.WriteLine( "spyOn = true" );
		else
			Console.WriteLine( "spyOn = false" );
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
