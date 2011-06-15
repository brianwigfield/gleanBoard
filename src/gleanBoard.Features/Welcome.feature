Feature: Welcome
	In order to feel welcome
	A as user
	I want to see a greeting message

@UX
Scenario: I am not logged in
	When I visit the homepage
	Then I should see a welcome message
