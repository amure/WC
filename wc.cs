// It is the project entry. form CPAPA p.63 CN.ver
//[:syn on] [:set nu] [:set cindent]
using System;
using System.IO;// it is need to read & write a file p17
public class WordCountEntry
{
	static public void Main( string [] args)
	{
		bool traceOn = false; //There is a code block about foreach in P14
		bool spyOn = false;
		string file_name = null; //code from p17 
		
		Console.WriteLine("Beginning WordCount program ... ");

		//����������ӽ��ܲ����Ĺ��ܣ��μ�12ҳ
		if (args.Length == 0 )
		{
			display_usage();//This function code is in P18.
			return;
		}else{	//There is a code block about foreach in P14
			//bool traceOn = false;
			//bool spyOn = false;
			//д�������Ӧ�������Դ��ˣ���ʵ�ϣ���������������Ҳ��ò�д������
			//string file_name = null; //code from p17

			foreach ( string option in args )
			{
				if ( option.Equals ( "-t" ))
					traceOn = true;
				else if (option.Equals ( "-s" ))
					spyOn = true;
				else if (option.Equals ( "-h" ))
				{display_usage(); return;}
				else
				{
					Console.WriteLine( @"check_valid_file_type( option );" );
					file_name = option;//transmit filename string into file_name
				}
				
			}
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
		//}


		WordCount theObj = new WordCount();
		//transmit filename into processFile
		//theObj.processFile();����ѡ�񴫵ݸ�������������ݸ����캯���ĵȵ��˺����ڿ���
		//���������ʽ�Ƚϼ�
		theObj.processFile( file_name );

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
	StreamReader freader = null;
	StreamWriter fwriter = null;
	public void processFile(string theFile)//accept a filename from WCE
	{
		openFiles( theFile );
		readFiles();
		countWords();
		writeWords();
	}

	private void countWords()
	{
		Console.WriteLine( "!!! WordCount.countWords() " );
	}
	
	//1.6 Formatting Output 01-p19 N.01061901
	//writing to the console indicate the line number and the length in characters	
	private void readFiles()
	{
		Console.WriteLine( "!!! WordCount.readFiles() " );
		//read freader that is a StramReader p18
		string text_line;
		int line_cnt = 1; //N.01061901
		
		while (( text_line = freader.ReadLine() ) != null )
		{
			//dont format empty lines
			if ( text_line.Length == 0 )
			{
				Console.WriteLine();
				continue;
			}//N.01061901
			//write to output file
			fwriter.WriteLine ( text_line );
			//format output to console;
			Console.WriteLine ( "{0} ({2}): {1}", line_cnt++, text_line, text_line.Length );
			//N.01061901
			//hjkl HML Y pP
		}
		//must explicitly close the readers
		freader.Close();
		fwriter.Close();
		//end in p18
	}
	
	private void openFiles(string theFile)//accept a filename from pF
	{
		Console.WriteLine( "!!! WordCount.openFiles() " );

		//copy code from p17
		//StreamReader freader = File.OpenText ( file_name );
		freader = File.OpenText ( theFile ); 
		//StreamWriter fwriter = File.CreateText ( "wc.diag" );
		fwriter = File.CreateText ( "wc.diag" );
		
		//���������ݳ�Ա��򵥵ľ��Ǵ��ݸ�readFiles����rF����ʵ��p18��19����򵥵Ļ�������
		//�Ȳ�֪�ú�����ڵķ��������ڵ�ǰ�ṹ�£���򵥵Ľ����������������freader&fw�ķ��ʼ���
	}
	
	private void writeWords()
	{
		Console.WriteLine( "!!! WordCount.writeWords() " );
	}
}
