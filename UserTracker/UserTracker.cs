﻿/*
 * FOG Service : A computer management client for the FOG Project
 * Copyright (C) 2015 FOG Project
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 3
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */
 
using System;
using System.Collections.Generic;
using System.Net;

namespace FOG
{
    /// <summary>
    /// Report what users log on or off and at what time
    /// </summary>
    public class UserTracker: AbstractModule
    {
        List<String> usernames;
        
        public UserTracker() 
        {
            Name = "UserTracker";
            Description = "Tracker user logins and logouts";
            usernames = new List<String>();
        }
        
        protected override void doWork()
        {
            var currentUsers = UserHandler.GetUsersLoggedIn();
            
            foreach(String username in currentUsers)
            {
                if(usernames.Contains(username))
                {
                    usernames.Remove(username);
                } else {
                    CommunicationHandler.Contact("/service/usertracking.report.php?action=login&user=" + Dns.GetHostName() + "\\" + username, true);
                }
            }
            
            foreach(String username in usernames)
            {
                 CommunicationHandler.Contact("/service/usertracking.report.php?action=logout&user=" + Dns.GetHostName() + "\\" + username, true);
            }
            
            
            usernames = currentUsers;
        }
        
    }
}