package com.sagioto.lotto;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.firefox.FirefoxDriver;


public class Main {

	public static void main(String [] args) throws InterruptedException {
		List<Integer> selectedNumbers = new ArrayList<Integer>();
		List<Integer> numbers = new ArrayList<Integer>();
		HashMap<Integer, Integer> hotNumbers = new HashMap<Integer, Integer>();
		HashMap<Integer, Integer> hotStrong = new HashMap<Integer, Integer>();



		randomSelection(selectedNumbers, numbers, hotNumbers, hotStrong);
		clearAll(selectedNumbers, numbers, hotNumbers, hotStrong);
		
		staticStatisticalSelection(selectedNumbers, numbers, hotNumbers, hotStrong);
		clearAll(selectedNumbers, numbers, hotNumbers, hotStrong);
		
		WebDriver driver = getDataFromWeb(hotNumbers, hotStrong);
		
		
		
		Integer selectedStrong = calculateSelection(selectedNumbers,
				numbers, hotNumbers, hotStrong);

		Collections.sort(selectedNumbers);
		System.out.println("from site statistical:  " + selectedNumbers + " [" + selectedStrong + "]");
		
		driver.close();
	}

	private static WebDriver getDataFromWeb(
			HashMap<Integer, Integer> hotNumbers,
			HashMap<Integer, Integer> hotStrong) throws InterruptedException {
		WebDriver driver = new FirefoxDriver();
		
		doClicks(driver);	
		parseTables(hotNumbers, hotStrong, driver);
	
		return driver;
	}

	private static void parseTables(HashMap<Integer, Integer> hotNumbers,
			HashMap<Integer, Integer> hotStrong, WebDriver driver) {
		String start = "/html/body/form/div[2]/div[3]/div[3]/div/div[3]/table/tbody/tr";
		String trIndex = "";
		String tdIndex = "";
		String text;
		
		int row = 1;
		
		for (int i = 1; i <= 37; i++) {
			if(i % 4 == 1 && i != 1){
				tdIndex = "";
				trIndex = String.valueOf("[" + ++row + "]");
			}
			else if (i != 1){
				if(i % 4 == 0)
					tdIndex = String.valueOf("[4]");
				else
					tdIndex = String.valueOf("[" + (i % 4) + "]");
			}
			text = driver.findElement(By.xpath(start + trIndex +"/td" + tdIndex + "/ul/li[2]")).getText();
			hotNumbers.put(i, getNumber(text));
		}
		
		start = "/html/body/form/div[2]/div[3]/div[3]/div/div[3]/table[2]/tbody/tr";
		trIndex = "";
		tdIndex = "";
		
		row = 1;
		
		for (int i = 1; i <= 7; i++) {
			if(i % 4 == 1 && i != 1){
				tdIndex = "";
				trIndex = String.valueOf("[" + ++row + "]");
			}
			else if (i != 1){
				if(i % 4 == 0)
					tdIndex = String.valueOf("[4]");
				else
					tdIndex = String.valueOf("[" + (i % 4) + "]");
			}
			text = driver.findElement(By.xpath(start + trIndex +"/td" + tdIndex + "/ul/li[2]")).getText();
			hotStrong.put(i, getNumber(text));
		}
	}

	private static void doClicks(WebDriver driver) throws InterruptedException {


		driver.get("http://www.pais.co.il/Lotto/Pages/Statistics.aspx");
		driver.findElement(By.id("PaisRangeSearchTypeDate"));
		WebElement radio = driver.findElement(By.id("PaisFromDate"));
		radio.click();
		radio.isSelected();
		radio.click();
		driver.findElement(By.id("/html/body/div/div/div/select[2]")).sendKeys("2009");
		driver.findElement(By.xpath("/html/body/div/div/div/select")).sendKeys("2");
		driver.findElement(By.xpath("/html/body/div/div/div/select")).sendKeys("2");
		driver.findElement(By.xpath("/html/body/div/table/tbody/tr[4]/td[7]/a")).click();
		driver.findElement(By.xpath("/html/body/div/table/tbody/tr[4]/td[7]/a")).click();


		driver.findElement(By.xpath("/html/body/form/div[2]/div[3]/div[3]/div/div/div[2]/div[3]/button")).click();

		Thread.sleep(1500);
	}

	private static void clearAll(List<Integer> selectedNumbers,
			List<Integer> numbers, HashMap<Integer, Integer> hotNumbers,
			HashMap<Integer, Integer> hotStrong) {
		selectedNumbers.clear();
		numbers.clear();
		hotNumbers.clear();
		hotStrong.clear();
	}

	private static Integer getNumber(String text) {
		return Integer.valueOf(text.split("\n")[0]);
	}

	private static void staticStatisticalSelection(
			List<Integer> selectedNumbers, List<Integer> numbers,
			HashMap<Integer, Integer> hotNumbers,
			HashMap<Integer, Integer> hotStrong) {
		hotNumbers.put(1, 240);
		hotNumbers.put(2, 247);
		hotNumbers.put(3, 237);
		hotNumbers.put(4, 235);
		hotNumbers.put(5, 213);
		hotNumbers.put(6, 234);
		hotNumbers.put(7, 238);
		hotNumbers.put(8, 268);
		hotNumbers.put(9, 247);
		hotNumbers.put(10, 235);
		hotNumbers.put(11, 262);
		hotNumbers.put(12, 247);
		hotNumbers.put(13, 218);
		hotNumbers.put(14, 225);
		hotNumbers.put(15, 217);
		hotNumbers.put(16, 236);
		hotNumbers.put(17, 242);
		hotNumbers.put(18, 227);
		hotNumbers.put(19, 227);
		hotNumbers.put(20, 251);
		hotNumbers.put(21, 260);
		hotNumbers.put(22, 229);
		hotNumbers.put(23, 223);
		hotNumbers.put(24, 254);
		hotNumbers.put(25, 227);
		hotNumbers.put(26, 238);
		hotNumbers.put(27, 217);
		hotNumbers.put(28, 233);
		hotNumbers.put(29, 222);
		hotNumbers.put(30, 234);
		hotNumbers.put(31, 239);
		hotNumbers.put(32, 232);
		hotNumbers.put(33, 214);
		hotNumbers.put(34, 216);
		hotNumbers.put(35, 155);
		hotNumbers.put(36, 118);
		hotNumbers.put(37, 132);



		hotStrong.put(1, 98);
		hotStrong.put(2, 112);
		hotStrong.put(3, 107);
		hotStrong.put(4, 107);
		hotStrong.put(5, 104);
		hotStrong.put(6, 97);
		hotStrong.put(7, 114);


		selectedNumbers.clear();

		Integer selectedStrong = calculateSelection(selectedNumbers,
				numbers, hotNumbers, hotStrong);

		Collections.sort(selectedNumbers);
		System.out.println("static statistical: 	" + selectedNumbers + " [" + selectedStrong + "]");
		
		
	}

	private static Integer calculateSelection(
			List<Integer> selectedNumbers, List<Integer> numbers,
			HashMap<Integer, Integer> appearenceNumbers,
			HashMap<Integer, Integer> hotStrong) 
	{
		for(int i = 1; i <= 37; i++){
			for (int j = 0; j < appearenceNumbers.get(i); j++) {
				numbers.add(i);
			}
		}

		for (int i = 0; i < 6; i++) {
			Integer selected = numbers.get((int)(Math.random() * numbers.size()));
			ArrayList<Integer> toRemove = new ArrayList<Integer>();
			toRemove.add(selected);
			numbers.removeAll(toRemove);
			selectedNumbers.add(selected);
		}

		numbers.clear();

		for(int i = 1; i <= 7; i++){
			for (int j = 0; j < hotStrong.get(i); j++) {
				numbers.add(i);
			}
		}
		Integer selectedStrong = numbers.get((int)(Math.random() * numbers.size()));
		return selectedStrong;
	}

	
	private static void randomSelection(
			List<Integer> selectedNumbers, List<Integer> numbers,
			HashMap<Integer, Integer> hotNumbers,
			HashMap<Integer, Integer> hotStrong)
	{
		for (int i = 1; i <= 37; i++) {
			hotNumbers.put(i, 1);
		}
		for (int i = 1; i <= 7; i++) {
			hotStrong.put(i, 1);
		}
		
		int selectedStrong = calculateSelection(selectedNumbers, numbers, hotNumbers, hotStrong);

		Collections.sort(selectedNumbers);
		System.out.println("\n" + "random: 		" + selectedNumbers + " [" + selectedStrong + "]");
	}

}
