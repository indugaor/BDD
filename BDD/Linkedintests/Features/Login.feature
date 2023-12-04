Feature: Login
User logs in with valid credentials(username, password)
the home page will load after sucessful login

Background: 
	Given  User will be on the login page

@positive
Scenario Outline: Login with Valid Credentials
	When User will enter '<UserName>'
	And User will enter '<Password>'
	And User will click on login button
	Then User will be redirected to Homepage
Examples: 
	| UserName    | Password |
	| abc@xyz.com | 12345    |
	| def@xyz.com | 98765    |

@negative
Scenario: Login with Invalid Credentials
	
	When User will enter username
	And User will enter password
	And User will click on login button
	Then Error message for Password Length should be thrown

@regression
Scenario: Check for password Hidden Display

	When User will enter password
	And User will click on Show link in the pasword textbox
	Then the password characters should not shown

@regression
Scenario: Check for password shown Display
	
	When User will enter password
	And User will click on Show link in the pasword textbox
	Then the password characters should not shown

