using ClueLess.Database.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ClueLess.Database
{
    public class ClueLessContext : DbContext
    {
        public ClueLessContext() : base("ClueLessContext") {
           Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Actions> Actions { get; set; }
        public DbSet<ActionTaken> ActionsTaken { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterConfiguration> CharacterConfigurations { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameSolution> GameSolutions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerToCharacter> PlayersToCharcters { get; set; }
        public DbSet<PlayerToLocation> PlayersToLocations { get; set; }
        public DbSet<PlayerToWeapon> PlayersToWeapons { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<SecretPassages> SecretPassages { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<SuggestionResponse> SuggestionResponses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<WeaponConfiguration>WeaponConfigurations{get;set;} 
        public DbSet<WeaponPosition> WeaponPositions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<Actions>().HasKey(a => a.ID);
            //modelBuilder.Entity<Actions>().HasMany(a => a.ActionsTaken);

            //modelBuilder.Entity<ActionTaken>().HasKey(at => at.ID);
            //modelBuilder.Entity<ActionTaken>().HasRequired(at => at.Player);
            //modelBuilder.Entity<ActionTaken>().HasRequired(at => at.Action);
            ////modelBuilder.Entity<ActionTaken>().HasOptional(at => at.Position);
            ////modelBuilder.Entity<ActionTaken>().HasOptional(at => at.Suggestion);
            //modelBuilder.Entity<ActionTaken>().HasOptional(at => at.MovedPlayer);

            //modelBuilder.Entity<Character>().HasKey(ch => ch.ID);
            //modelBuilder.Entity<Character>().HasMany(ch => ch.CharacterConfiguration);

            //modelBuilder.Entity<CharacterConfiguration>().HasKey(cc => cc.ID);
            //modelBuilder.Entity<CharacterConfiguration>().HasRequired(cc => cc.Character);
            //modelBuilder.Entity<CharacterConfiguration>().HasRequired(cc => cc.Configuration);
            modelBuilder.Entity<CharacterConfiguration>().HasRequired(cc => cc.StartingPosition).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<CharacterConfiguration>().HasMany(cc => cc.Solutions);
            //modelBuilder.Entity<CharacterConfiguration>().HasMany(cc => cc.Players);
            //modelBuilder.Entity<CharacterConfiguration>().HasMany(cc => cc.CharacterClues);
            //modelBuilder.Entity<CharacterConfiguration>().HasMany(cc => cc.Suggestions);

            modelBuilder.Entity<Configuration>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);

            //modelBuilder.Entity<Game>().HasKey(g => g.ID);
            //modelBuilder.Entity<Game>().HasMany(g => g.Players);
            //modelBuilder.Entity<Game>().HasMany(g => g.Weapons);
            modelBuilder.Entity<Game>().HasRequired(g => g.Configuration).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Game>().HasRequired(g => g.GameOwner).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Game>().HasRequired(g => g.Solution);

            //modelBuilder.Entity<GameSolution>().HasKey(gs => gs.ID);
            //modelBuilder.Entity<GameSolution>().HasRequired(gs => gs.Game);
            //modelBuilder.Entity<GameSolution>().HasRequired(gs => gs.Weapon);
            //modelBuilder.Entity<GameSolution>().HasRequired(gs => gs.Character);
            //modelBuilder.Entity<GameSolution>().HasRequired(gs => gs.Location);
            //modelBuilder.Entity<GameSolution>().HasOptional(gs => gs.Winner);

            //modelBuilder.Entity<Location>().HasKey(l => l.ID);
            //modelBuilder.Entity<Location>().HasMany(l => l.Positions);

            //modelBuilder.Entity<Player>().HasKey(p => p.ID);
            //modelBuilder.Entity<Player>().HasRequired(p => p.User);
            //modelBuilder.Entity<Player>().HasRequired(p => p.Game);
            //modelBuilder.Entity<Player>().HasRequired(p => p.Position);
            //modelBuilder.Entity<Player>().HasRequired(p => p.Character);
            //modelBuilder.Entity<Player>().HasMany(p => p.WeaponClues);
            //modelBuilder.Entity<Player>().HasMany(p => p.LocationClues);
            //modelBuilder.Entity<Player>().HasMany(p => p.CharacterClues);
            //modelBuilder.Entity<Player>().HasMany(p => p.Suggestions);
            //modelBuilder.Entity<Player>().HasMany(p => p.ActionsTaken);
            //modelBuilder.Entity<Player>().HasMany(p => p.SuggestionResponses);
            modelBuilder.Entity<Player>().HasRequired(p => p.Position).WithMany().WillCascadeOnDelete(false);

            //modelBuilder.Entity<PlayerToCharacter>().HasKey(ptc => ptc.ID);
            modelBuilder.Entity<PlayerToCharacter>().HasRequired(ptc => ptc.Player).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<PlayerToCharacter>().HasRequired(ptc => ptc.CharacterClue);

            //modelBuilder.Entity<PlayerToLocation>().HasKey(ptl => ptl.ID);
            //modelBuilder.Entity<PlayerToLocation>().HasRequired(ptl => ptl.Player);
            //modelBuilder.Entity<PlayerToLocation>().HasRequired(ptl => ptl.LocationClue);

            modelBuilder.Entity<PlayerToWeapon>().HasKey(plw => plw.ID);
            modelBuilder.Entity<PlayerToWeapon>().HasRequired(plw => plw.Player).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PlayerToWeapon>().HasRequired(plw => plw.WeaponClue).WithMany().WillCascadeOnDelete(false);

            //modelBuilder.Entity<Position>().HasKey(po => po.ID);
            //modelBuilder.Entity<Position>().HasRequired(po => po.Location);
            modelBuilder.Entity<Position>().HasRequired(po => po.Configuration).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Position>().HasMany(po => po.GameSolutions);
            //modelBuilder.Entity<Position>().HasMany(po => po.LocationClues);
            //modelBuilder.Entity<Position>().HasMany(po => po.Suggestion);
            //modelBuilder.Entity<Position>().HasMany(po => po.ActionsTaken);
            //modelBuilder.Entity<Position>().HasMany(po => po.SecretPassages);
            

            //modelBuilder.Entity<SecretPassages>().HasKey(sp => sp.ID);
            //modelBuilder.Entity<SecretPassages>().HasRequired(sp => sp.Room1);
            //modelBuilder.Entity<SecretPassages>().HasRequired(sp => sp.Room2);

            //modelBuilder.Entity<Suggestion>().HasKey(s => s.ID);
            modelBuilder.Entity<Suggestion>().HasRequired(s => s.Player).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Suggestion>().HasRequired(s => s.Location).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Suggestion>().HasRequired(s => s.Weapon).WithMany().WillCascadeOnDelete(false) ;
            modelBuilder.Entity<Suggestion>().HasRequired(s => s.Character).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Suggestion>().HasMany(s => s.Reponses);

            //modelBuilder.Entity<SuggestionResponse>().HasKey(sr => sr.ID);
            //modelBuilder.Entity<SuggestionResponse>().HasRequired(sr => sr.Suggestion);
            //modelBuilder.Entity<SuggestionResponse>().HasRequired(sr => sr.RespondingPlayer);

            //modelBuilder.Entity<User>().HasKey(u => u.ID);
            //modelBuilder.Entity<User>().HasMany(u => u.Configuration);
            //modelBuilder.Entity<User>().HasMany(u => u.Game);
            //modelBuilder.Entity<User>().HasMany(u => u.Player);
            //modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            //modelBuilder.Entity<Weapon>().HasKey(w => w.ID);
            //modelBuilder.Entity<Weapon>().HasMany(w => w.WeaponConfigurations);

            //modelBuilder.Entity<WeaponConfiguration>().HasKey(wc => wc.ID);
            //modelBuilder.Entity<WeaponConfiguration>().HasRequired(wc => wc.Configuration);
            //modelBuilder.Entity<WeaponConfiguration>().HasRequired(wc => wc.Weapon);
            //modelBuilder.Entity<WeaponConfiguration>().HasMany(wc => wc.WeaponClues);
            //modelBuilder.Entity<WeaponConfiguration>().HasMany(wc => wc.Suggestions);
            //modelBuilder.Entity<WeaponConfiguration>().HasMany(wc => wc.Solutions);

            //modelBuilder.Entity<WeaponPosition>().HasKey(wp => wp.ID);
            //modelBuilder.Entity<WeaponPosition>().HasRequired(wp => wp.Game);
            //modelBuilder.Entity<WeaponPosition>().HasRequired(wp => wp.ConfiguredWeapon);
            //modelBuilder.Entity<WeaponPosition>().HasRequired(wp => wp.Position);

            //using (var context = new ClueLessContext())
            //{
            //    context.Database.CreateIfNotExists();
            //}

            
        }
    }
}