/*
 * 
 * Proj 6
 * 
 * Joseph Medina Thursday 6-745pm Atef Khan
 * 
 * Weighted Grades 
 */
import java.util.*;
import java.text.*;
public class Student {
	
	public String fullName;
	public String wid;
	public double finalGrade;
	
	//default no arg constructor
	public Student () {
		 fullName = "no name entered";
		 wid = "no WID";
	}
	
	
	//multi-arg constructr
	public Student (String f, String w, double fG) {
		fullName = f;
		wid = w;
		finalGrade = fG;
	}
	
	
	private static double labWeight = .10;
	private static double projectWeight = .15;
	private static double examWeight = .45;
	private static double codeLabWeight = .10;
	private static double finalWeight = .20;
	
	
	
	
	public String toString() {
		DecimalFormat df = new DecimalFormat ("##.#%");
		
		
		
		
		return ("\nStudent Name: " + fullName +
				"\nWID: " + wid +
				"\nOverall %: " +  df.format(finalGrade) +
				"\nFinal Grade: " + calcLetterGrade(finalGrade) + "\n");
	} 
	
	/**
	* method to calculate total points
	*
	*@param- labPoints, total points possible in lab section
	*@param- projectPoints, total points possible in project section
	*@param- examPoints, total points possible in exam section
	*@param- codeLabPoints, total points possible in code lab section
	*@param- finalPoints, total points  in final section
	*@param- studentLab, points in lab section
	*@param- studentProject, points in project section
	*@param- studentExam, points in exam section
	*@param- studentCodeLab, points in code lab section
	*@param- studentFinal,  points in final section
	*@return- returns calcGrade to get final grade in class
	*/
	
	public static double calcPoints (double labPoints, double projectPoints, double examPoints, double codeLabPoints, double finalPoints, double studentLab, double studentProject, double studentExam, double studentCodeLab, double studentFinal) {
		double totalPoints = labPoints + projectPoints + examPoints + codeLabPoints + finalPoints;
		
		double adjLab = totalPoints * labWeight;		
		double adjLabPoints = ((studentLab/labPoints) * adjLab);
		
		double adjProj = totalPoints * projectWeight;
		double adjProjPoints = ((studentProject/projectPoints) * adjProj);
		
		double adjCodeLab = totalPoints * codeLabWeight;
		double adjCodeLabPoints = ((studentCodeLab/codeLabPoints) * adjCodeLab);
		
		double adjExam = totalPoints * examWeight;
		double adjExamPoints = ((studentExam/examPoints) * adjExam);
		
		
		double adjFinal = totalPoints * finalWeight;
		double adjFinalPoints = ((studentFinal/finalPoints) * adjFinal);
		
		double adjPointsFinal = adjLabPoints + adjProjPoints + adjExamPoints + adjCodeLabPoints + adjFinalPoints;
		
		
		return calcGrade(adjPointsFinal, totalPoints);
	}
	
	/**
	 * (displays final grade)
	 *
	 * @adjPointsFinal (adjusted weight of grade)
	 * @totalPoints (total points overall)
	 * (adj Points Final, totalPoints)
	 * @return (finalGrade is a double that holds the final number grade)
	 */ 
	
	private static double calcGrade(double adjPointsFinal, double totalPoints) {
		
		double finalGrade = (adjPointsFinal/totalPoints);
		
		return finalGrade;
	}
	

	/**
	 * (lists the letter grade for the number grade passed in)
	 *
	 * @finalGrade (number grade calculated earlier)
	 * 
	 * (finalGrade)
	 * @return (A letter is returned)
	 */ 
	private static String calcLetterGrade (double finalGrade) {
		String letterGrade = null;
		finalGrade = finalGrade * 100;
		if (finalGrade > 100) {
			letterGrade = "A+";
		}
		else if (finalGrade >= 89.5 && finalGrade <= 100)
			letterGrade = "A";
		else if (finalGrade >= 79.5 && finalGrade < 89.5)
			letterGrade = "B";
		else if (finalGrade >= 68.5 && finalGrade < 79.5)
			letterGrade = "C";
		else if (finalGrade >= 58.5 && finalGrade < 68.5)
			letterGrade = "D";
		else if (finalGrade >= 0 && finalGrade < 58.5)
			letterGrade = "F";
		
		return letterGrade;
	}
}