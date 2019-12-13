
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace LogServer
{
    public class Main : BaseScript
    {
        public Main()
        {
            EventHandlers.Add("playerConnecting", new Action<Player, string, CallbackDelegate>(OnPlayerConnecting));
            EventHandlers.Add("playerDropped", new Action<Player, string>(OnPlayerDisconnect));
        }

        private void OnPlayerConnecting([FromSource] Player player, string playerName, CallbackDelegate kickCallback)
        {
            Debug.WriteLine($"^7");
            Debug.WriteLine($"^5-- [INFO] -- ---------------------------------------------------------");
            Debug.WriteLine($"^5-- [INFO] -- Nome Steam: {player.Name}");
            Debug.WriteLine($"^5-- [INFO] -- ID Steam: steam:{player.Identifiers["steam"]}");
            Debug.WriteLine($"^5-- [INFO] -- Licenza: license:{player.Identifiers["license"]}");
            Debug.WriteLine($"^5-- [INFO] -- DiscordID:  discord:{player.Identifiers["discord"]}");
            Debug.WriteLine($"^5-- [INFO] -- Indirizzo IP:  ip:{player.EndPoint.ToString()}");
            Debug.WriteLine($"^5-- [INFO] -- ---------------------------------------------------------");
            Debug.WriteLine($"^7");

            string percorso = "LogServer.txt";

            if (!File.Exists(percorso))
            {
                string creazione = $"{player.Name} sta entrando in sessione con il seguente IP = {player.EndPoint.ToString()}, steam:{player.Identifiers["steam"]}, licenza:{player.Identifiers["license"]}, discord:{player.Identifiers["discord"]}, il {DateTime.Now}" + Environment.NewLine;
                File.WriteAllText(percorso, creazione);
            }

            string aggiunta = $"{player.Name} sta entrando in sessione con il seguente IP = {player.EndPoint.ToString()}, steam:{player.Identifiers["steam"]}, licenza:{player.Identifiers["license"]}, discord:{player.Identifiers["discord"]}, il {DateTime.Now}" + Environment.NewLine;
            File.AppendAllText(percorso, aggiunta);
        }

        public void OnPlayerDisconnect([FromSource] Player player, string reason)
        {
            Debug.WriteLine($"^7");
            Debug.WriteLine($"^8-- [INFO] -- ---------------------------------------------------------");
            Debug.WriteLine($"^8-- [INFO] -- {player.Name} ha abbandonato la sessione => Motivo : {reason}");
            Debug.WriteLine($"^8-- [INFO] -- ---------------------------------------------------------");
            Debug.WriteLine($"^7");

            string percorso = "LogServer.txt";

            if (!File.Exists(percorso))
            {
                string creazione = $"{player.Name} ha abbandonato la sessione (motivo: {reason} il {DateTime.Now})" + Environment.NewLine;
                File.WriteAllText(percorso, creazione);
            }

            string aggiunta = $"{player.Name} ha abbandonato la sessione (motivo: {reason} il {DateTime.Now})" + Environment.NewLine;
            File.AppendAllText(percorso, aggiunta);
        }
    }
}