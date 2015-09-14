using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using GWvW_Overlay_Location_Server.AnetAPI;
using GWvW_Overlay_Location_Server.AnetAPI.Resources;
using GWvW_Overlay_Location_Server.Annotations;
using GWvW_Overlay_Location_Server.Properties;
using GWvW_Overlay_Location_Server_Contracts;
using Timer = System.Timers.Timer;

namespace GWvW_Overlay_Location_Server
{
    public class LocationService : ILocationService, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private static readonly HybridDictionary CallbackChannels = new HybridDictionary();
        private static readonly HybridDictionary Clients = new HybridDictionary();
        private readonly Timer _updateLocationsTimer = new Timer(Settings.Default.timer_timelapse);

        private static readonly List<int> ServerIds = new List<int>();
        private static readonly List<int> MapIds = new List<int>();


        public LocationService()
        {
            _updateLocationsTimer.Elapsed += UpdateLocationsTimerOnElapsed;
            _updateLocationsTimer.Start();
        }

        private static void UpdateLocationsTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            foreach (var server in ServerIds)
            {
                foreach (var mapId in MapIds)
                {
                    var id = mapId;
                    var serverId = server;
                    var mates = Clients.Values.AsParallel().Cast<Client>().Where(c => c.ServerId == serverId && c.MapId == id);

                    mates.ForAll(
                        async c =>
                        {
                            await ((ILocationServiceCallBack)CallbackChannels[c.Id]).ReceivePositions(mates.Select(cl => cl.Position).ToList());
                        });
                }
            }
        }


        public Guid SubscribeClient(Client newClient)
        {
            var channel = OperationContext.Current.GetCallbackChannel<ILocationServiceCallBack>();
            newClient.Id = Guid.NewGuid();

            var account = Request.GetResource<Account>(newClient.AnetAccountApiKey);

            if (!ServerIds.Contains(account.World))
            {
                ServerIds.Add(account.World);
            }

            if (!MapIds.Contains(newClient.MapId))
            {
                MapIds.Add(newClient.MapId);
            }

            var existing = Clients.Values.AsParallel().Cast<Client>().FirstOrDefault(c => c.AnetAccountApiKey == newClient.AnetAccountApiKey ||
                                                                                     c.AccountName == account.Name);




            if (existing != null)
            {

                if (!CallbackChannels.Values.AsParallel().Cast<Client>().Contains(existing))
                {
                    CallbackChannels.Add(existing.Id, channel);
                }

                return existing.Id;
            }

            while (Clients.Contains(newClient.Id))
            {
                newClient.Id = Guid.NewGuid();
            }


            newClient.AccountName = account.Name;
            newClient.AccountId = account.Id;
            newClient.ServerId = account.World;

            Clients.Add(newClient.Id, newClient);


            if (!CallbackChannels.Contains(newClient.Id))
            {
                CallbackChannels.Add(newClient.Id, channel);
            }



            return newClient.Id;
        }

        public void SendPosition(Guid clientId, Position position)
        {
            if (Clients.Contains(clientId))
            {
                ((Client)Clients[clientId]).Position = position;
            }
        }

        public void UnsubscribeClient(Guid clientId)
        {
            Clients.Remove(clientId);
            CallbackChannels.Remove(clientId);
        }
    }
}