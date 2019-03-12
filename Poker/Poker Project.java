//*******************************************************************
//* Proj4.java
//* Joseph Medina
//*
//* This program resembles poker.
//*******************************************************************/

import java.util.*;


public class Proj4 {
    
	public static void main(String[] args){
		Scanner s = new Scanner (System.in);
		
		char answer;
		
		
		System.out.println("** Welcome to the 2017 Las Vegas Poker Festival! ** \n   (Application developed by Joseph Medina)\n");

		do {
			String cardSuit;
			String faceCard = "";
			int hand = 0;
			boolean dup = false;
		
			boolean high = false;
			System.out.println("Shuffling cards...\nDealingCards...\n");

			System.out.println("Here are your five cards...");

			Random random = new Random();
			
			int [] value = new int [5]; //COMMENT THIS OUT FOR HARDCODED VALUES (1 of 4) SEE LINES 77 AND 78
			int [] suit = new int [5]; //COMMENT THIS OUT FOR HARDCODED VALUES (2 of 4) SEE LINES 77 AND 78
		
			//Royal Flush 
		//int[] value = {10, 12, 14, 13, 11}; 
		//int[] suit = {1,1,1,1,1};

		// Straight Flush 
		 // int[] value = {9, 7, 8, 6, 5}; 
		  //int[] suit = {1,1,1,1,1};

		// 4 of kind 
		 //int[] value = {9, 9, 9, 9, 7}; 
		 //int[] suit = {1,2,3,4,1};
		
		// Full House 
		 // int[] value = {7, 9 , 9, 7, 9}; 
		  //int[] suit = {1,2,3,4,1};

		// Flush 
		 // int[] value = {9, 10, 8, 6, 5}; 
		 // int[] suit = {1,1,1,1,1};

		// Straight 
		 // int[] value = {9, 7, 8, 6, 5}; 
		 // int[] suit = {1,2,4,3,1};

		// 3 of kind 
		 // int[] value = {9, 9, 9, 2, 7}; 
		 //int[] suit = {1,2,3,4,1};

		// Two Pair 
		  //int[] value = {9, 7, 9, 2, 7}; 
		 //int[] suit = {1,2,3,4,1};

		// One Pair 
		  //int[] value = {9, 7, 8, 2, 7}; 
		  //int[] suit = {1,2,3,4,1};

		// High Card (Ace) 
		//int [] value = {9, 7, 8, 14, 11}; 
		//int [] suit = {1,2,3,4,1};
			
			for (int j=0; j < value.length; j++){
				
				while (dup == false){
				value [j] = random.nextInt(13) + 2; //COMMENT THIS OUT FOR HARDCODED VALUES SEE LINES 31 AND 32
					suit [j] = random.nextInt(4) + 1; // COMMENT THIS OUT FOR HARDCODED VALUES SEE LINES 31 AND 32
					
					
					dup = true;
					for (int e = 0; e < j; e++ ){
					
						if (e != j){
							if (value[j] == value[e] && suit[j] == suit[e]) {
								dup = false;	
							}
						}
			
						}
					}
				dup = false;
				}
				
			for (int i = 0; i <value.length; i++){
			    for (int j = 0; j < value.length; j++){
			        if (value[i] < value[j] && i < j){
			            int temp1 = suit[i];
			           int temp2 = value[i];

			            suit[i] = suit[j];
			            
			           value[i] = value[j];

			            suit[j] = temp1;
			           value[j] = temp2;
			            
			        }    
			    }   
			}
			 	
				
	 

	  for (int i=0; i<5; i++){
	  	
		  if (suit[i] == 1){
			  cardSuit = "Clubs"; }
		 
		  else if (suit[i] == 2) {
			  cardSuit = "Spades";	
		  }
	
		  else if (suit[i] == 3) {
			  cardSuit = "Diamonds";	
		  }
	
		  else {
			  cardSuit = "Hearts";
		  }

		  if (value[i] == 11){
			  faceCard = "Jack"; }
		  else if (value[i] == 12) {
			  faceCard = "Queen";	
		  }
	
		  else if (value[i] == 13) {
			  faceCard = "King";	
	
		  }
	
		  else if (value[i] == 14) {
			  faceCard = "Ace";
		  }

	
		  if (value[i] < 11){
			  System.out.println("\t" + value[i] + " of " + cardSuit );
		  }
	
		  else {
			  System.out.println("\t" + faceCard + " of " + cardSuit );
		  }
		  
		} // end of for loop

	  
	

if((value[0] == value[1] || value[1] == value[2] || value[2] == value[3] || value[3] == value[4] ) && (value[0] != value[2] && value [1] != value[3] && value[2] != value[4])  )
					{
	
						hand = 1;
					}


if((value[0] == value[1]) && (value[2] == value[3] || value[3] == value[4])  ){
	 high = true;
	hand = 2;
}

if((value[1] == value[2]) && (value[3] == value[4])){
	 high = true;
	hand = 2;
	
}

if ( ( value[0] == value[2] || value [1] == value [3]  || value[2] == value[4]) && (value[0] != value[3] && value[1] !=value[4]) ){
	 high = true;

	hand = 3;
}

for (int e = 14; e > suit.length; e--){
	int [] sflush = {e - 1 , e - 2, e - 3, e - 4, e - 5 };
	if (Arrays.equals(value, sflush)){
		 high = true;
		hand = 4;
		}
}

for (int i = 1; i < suit.length; i++ ){
	int [] same = {i,i,i,i,i};
	if (Arrays.equals(suit, same)){
		 high = true;
		hand = 5;
		}
}


if ( (value [0] == value [1] && value[2] == value[4]) || (value [0] == value [2] && value[3] == value[4]) ){
	 high = true;
	hand = 6;
}
	
for (int i = 0; i < 2; i++ ){
	
	if (value[i] == value[i+3]){
		 high = true;
		hand = 7;
	}
}

for (int i = 1; i < suit.length; i++ ){
	for (int e = 14; e > suit.length; e--){
		int [] sflush = {e - 1 , e - 2, e - 3, e - 4, e - 5 };
		int [] same = {i,i,i,i,i};
		if (Arrays.equals(value, sflush) && Arrays.equals(suit, same)){
			 high = true;
			hand = 8;
			}
	}
}



for (int i = 1; i < suit.length; i++ ){
				int [] royal = {14,13,12,11,10};
				int [] same = {i,i,i,i,i};
				if (Arrays.equals(value, royal) && Arrays.equals(suit, same)){
					 high = true;
					hand = 9;
					}
			}
			
			System.out.println();		
	switch(hand) {
		
		case 1: System.out.println("You were dealt a Pair");
				break;
		case 2: System.out.println("You were dealt a Two Pair");
				break;
		case 3: System.out.println("You were dealt a Three of a Kind");
		break;
		case 4: System.out.println("You were dealt a Straight");
		break;
		case 5: System.out.println("You were dealt a Flush");
		break;
		case 6: System.out.println("You were dealt a Full House");
		break;
		case 7: System.out.println("You were dealt a Four of a Kind");
		break;
		case 8: System.out.println("You were dealt a Straight Flush");
		break;
		case 9: System.out.println("You were dealt a Royal Flush");
		break;
		default:
			  if (value[0] == 11){
				  System.out.println("High card is a(n) Jack");  }
			  else if (value[0] == 12) {
				  System.out.println("High card is a(n) Queen"); 	
			  }
		
			  else if (value[0] == 13) {
				  System.out.println("High card is a(n) King"); 	
		
			  }
		
			  else if (value[0] == 14) {
				  System.out.println("High card is a(n) Ace"); 
			  }
			  else{
				  System.out.println("High card is a(n) " + value[0]);
			  }
			  break;
	}
					
					
	System.out.println();			
	
	  System.out.print("Play Again? (Y or N)?");
	  answer = s.nextLine().toUpperCase().charAt(0); 
	  if (answer != 'Y' || answer != 'N' ){
		  while (answer != 'Y' && answer != 'N'){
			  System.out.println("Please enter 'Y' or 'N' only"); 
			  System.out.print("Play Again? (Y or N)?");
			  answer = s.nextLine().toUpperCase().charAt(0);
		  };
	  }
	  System.out.println();
		}while (answer == 'Y'); //end do while
	
	} //end main
  
} // end class
