using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gamenight.Models;

namespace gamenight.Controllers
{
    public class GameController : Controller
    {
        private GameNightContext _context;
        public GameController( GameNightContext context )
        {
            _context = context;
        }

        public IActionResult AddNewGamePage()
        {
            return View( "AddPage" );
        }


        public IActionResult Dashboard()
        {
            if( HttpContext.Session.GetInt32( "UserId" ) == null )
            {
                return RedirectToAction( "Index", "Home" );
            }
            User RetrievedUser = _context.Users.SingleOrDefault( u => u.UserId == ( int ) HttpContext.Session.GetInt32( "UserId" ));

            List<Game> AllGames = _context.Games
                                                .OrderBy( rev => rev.Date )
                                                .Include( c => c.CreatedBy )
                                                .Include( w => w.Joiners )
                                                .ThenInclude( g => g.User )
                                                .ToList();

            ViewBag.LoggedId = HttpContext.Session.GetInt32( "UserId" );

            ViewBag.RetrievedUser = RetrievedUser;

            return View( "Success", AllGames );
        }


        public IActionResult CreateGameForm( GameViewModel model )
        {
            if( ModelState.IsValid )
            {
                User CurrentUser = _context.Users.Include( u => u.Games ).SingleOrDefault( u => u.UserId == ( int )HttpContext.Session.GetInt32( "UserId" ));

                    Game NewGame = new Game
                    {
                        Title = model.Title,
                        Platform = model.Platform,
                        Date = model.Date,
                        Time = model.Time,
                        Duration = model.Duration,
                        TimeSpan = model.TimeSpan,
                        Description = model.Description,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        CreatedById = CurrentUser.UserId
                    };

                    _context.Games.Add( NewGame );
                    _context.SaveChanges();

                    NewGame = _context.Games.SingleOrDefault( a => a.Description == model.Description );
                    HttpContext.Session.SetInt32( "GameId", NewGame.GameId );
                    return RedirectToAction( "ShowDetails", new { id = HttpContext.Session.GetInt32( "GameId" )});

            }
            return View( "AddPage" );
        }


        public IActionResult ShowDetails( int id )
        {
            if( HttpContext.Session.GetInt32( "UserId" ) == null )
            {
                return RedirectToAction( "Index", "Home" );
            }

            Game ThisGame = _context.Games
                                        .Include( c => c.CreatedBy )
                                        .Include( g => g.Joiners )
                                        .ThenInclude( u => u.User )
                                        .SingleOrDefault( w => w.GameId == id );


            List<Game> ThisParticularGame = _context.Games
                                                        .Include( c => c.CreatedBy )
                                                        .Include( w => w.Joiners )
                                                        .ThenInclude( g => g.User )
                                                        .Where( w => w.GameId == id )
                                                        .ToList();

            ViewBag.ThisParticularGame = ThisParticularGame;
            ViewBag.ThisGame = ThisGame;

            ViewBag.LoggedId = HttpContext.Session.GetInt32( "UserId" );

            return View( "Detail", ThisGame );
        }


        public IActionResult DeleteGame( int id )
        {
            Game x = _context.Games.SingleOrDefault( wed => wed.GameId == id );
            _context.Games.Remove( x );
            _context.SaveChanges();
            return RedirectToAction( "Dashboard" );
        }


        public IActionResult JoinGameGroup( int id )
        {
            Game x = _context.Games.Include( a => a.Joiners )
                                    .ThenInclude( a => a.User )
                                    .SingleOrDefault( a => a.GameId == id );

            Joiner gg = new Joiner{ Users_UserId = ( int ) HttpContext.Session.GetInt32( "UserId" ), Games_GameId = x.GameId };

            _context.Joiners.Add( gg );
            _context.SaveChanges();
            return RedirectToAction( "Dashboard" );
        }


        public IActionResult LeaveGameGroup( int id )
        {
            Joiner gg = _context.Joiners.SingleOrDefault( a => a.Users_UserId == ( int ) HttpContext.Session.GetInt32( "UserId" ) && a.Games_GameId == id );

            _context.Joiners.Remove( gg );
            _context.SaveChanges();
            return RedirectToAction( "Dashboard" );
        }
    }
}