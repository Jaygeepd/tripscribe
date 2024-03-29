﻿namespace tripscribe.Services.DTOs;

public class AccountDTO
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public IList<TripDTO>? Trips { get; set; }
}