﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatesAPI.Models;
using DatesAPI.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DatesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDetails model)
        {
            try
            {
                var token = await _userService.LoginAsync(model.Email, model.PasswordHash);
                return Ok(new { Token = token });
            }
            catch (Exception ex) when (ex.Message == $"No account found for {model.Email}! Please sign up first.")
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex) when (ex.Message == "Invalid credentials!")
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDetails model)
        {
            try
            {
                await _userService.RegisterAsync(model);
                return Ok("User registered successfully!");
            }
            catch (Exception ex) when (ex.Message == "User already exists!")
            {
                return Conflict(new {message = ex.Message});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
