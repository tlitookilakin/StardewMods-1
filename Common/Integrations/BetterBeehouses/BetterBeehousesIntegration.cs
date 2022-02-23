using StardewModdingAPI;
using StardewValley;

namespace Pathoschild.Stardew.Common.Integrations.BetterBeehouses
{
    internal class BetterBeehousesIntegration : BaseIntegration
    {
        /*********
       ** Fields
       *********/
        /// <summary>The mod's public API.</summary>
        private readonly IBetterBeehousesAPI ModApi;


        /*********
        ** Accessors
        *********/
        /// <summary>The Bee House coverage radius.</summary>
        public int MaxRadius => this.ModApi?.GetSearchRadius() ?? 5;

        /// <summary>How many days a bee house takes to produce.</summary>
        public int DaysToProduce => this.ModApi?.GetDaysToProduce() ?? 4;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="modRegistry">An API for fetching metadata about loaded mods.</param>
        /// <param name="monitor">Encapsulates monitoring and logging.</param>
        public BetterBeehousesIntegration(IModRegistry modRegistry, IMonitor monitor)
            : base("Better Beehouses", "tlitookilakin.BetterBeehouses", "1.1.2", modRegistry, monitor)
        {
            if (!this.IsLoaded)
                return;

            // get mod API
            this.ModApi = this.GetValidatedApi<IBetterBeehousesAPI>();
            this.IsLoaded = this.ModApi != null;
        }

        /// <summary>Get if bee houses can produce in this location right now.</summary>
        /// <param name="location"></param>
        /// <returns>True if bee houses are allowed to produce here, false if not, or if the mod is not loaded.</returns>
        public bool CanProduceHere(GameLocation location, bool isWinter)
        {
            return this.IsLoaded && this.ModApi.GetEnabledHere(location, isWinter);
        }
    }
}
