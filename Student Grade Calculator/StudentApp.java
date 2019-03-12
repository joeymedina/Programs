/*
 * 
 * Proj 6
 * 
 * Joseph Medina
 * 
 * Weighted Grades 
 */
import java.util.*;
import java.text.*;
public class StudentApp {
	public static void main (String[] args) {
		
		
	double labPoints = 0;
	double projectPoints = 0;
	double finalGrade = 0;
	double examPoints = 150;
	double codeLabPoints = 225;
	double finalPoints = 100;
	
	
	double studentLab = 0;
	double studentProject = 0;
	double studentExam = 0;
	double studentCodeLab = 0;
	double studentFinal = 0;
	int numStudents = 0;
	
	
	String fullName = "";
	
	String wid = "";
	
	
	String another = null;
	
	boolean anotherStudent = true;
	Student [] stu = new Student [50];
	
	
	Scanner s = new Scanner(System.in);
	
	System.out.print("Please enter the total number of points possible for Labs: ");
	labPoints = Double.parseDouble(s.nextLine());
	
	System.out.print("Please enter the total number of points possible for Projects: ");
	projectPoints = Double.parseDouble(s.nextLine());
	

	System.out.println();

	do {
		
		if (numStudents > 0) {
		System.out.print("");
		String string = s.nextLine();
		}
		
		System.out.print("Enter the student's full name: ");
		
		String full_Name = s.nextLine();
		fullName = convertName(full_Name);
		
		
		System.out.print("Enter the student's WID: ");
		wid = s.nextLine();			
		
		System.out.println();
		
		System.out.print("Enter student's total for all LABS: ");
		studentLab = Double.parseDouble(s.nextLine());
		
		System.out.print("Enter student's total for all PROJECTS: ");
		studentProject = Double.parseDouble(s.nextLine());
		
		System.out.print("Enter student's total for CODELAB: ");
		studentCodeLab = Double.parseDouble(s.nextLine());
		
		System.out.print("Enter student's total for THE 3 CLASS EXAMS: ");
		studentExam = Double.parseDouble(s.nextLine());
		
		
		System.out.print("Enter student's total for the FINAL EXAM: ");
		studentFinal = Double.parseDouble(s.nextLine());
		
		finalGrade = Student.calcPoints(labPoints, projectPoints, examPoints, codeLabPoints, finalPoints, studentLab, studentProject, studentExam, studentCodeLab, studentFinal);
		
		
		stu [numStudents] = new Student(fullName, wid, finalGrade);
		

	
		numStudents++;
		
		
		System.out.println();
		
		System.out.println(numStudents + " Student(s) entered so far." +
							"\n Up to 50 students can be entered" );
		System.out.print("Would you like to enter another student? ('Y' or 'N')");
		another = s.next().toUpperCase();
		
		if (another.equals("N")) {
			anotherStudent = false;
		}
		
		
		
		
	
		
	}while (anotherStudent == true);
	
	boolean flag = false;
	for (int i = 0; i < numStudents; i++)
	{
		
		
		if(stu[i] != null)
	{
			
		System.out.println(stu[i]);
		
		System.out.print("\t Press enter to display next student...");
		String enter = s.nextLine();
		if(flag == false)
		{
			enter = s.nextLine();
			flag = true;
		}
	}

		
		 
	}
	
	System.out.println("\nAll students displayed...");
	

	}
	
	/**
	 * (converts name to last comma first)
	 *
	 * @name (the fullname passed in)
	 * 
	 * (String name)
	 * @return (the string cName which is last comma first)
	 */ 
	  public static String convertName(String name) {
	        String firstName = name.substring(0, name.indexOf(" "));
	        String lastName = name.substring(name.indexOf(" "));
	        String cName =  lastName + ", " + firstName;
	        return cName;
	    }
	
}
