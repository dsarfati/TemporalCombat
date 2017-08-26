using UnityEngine;
using UniRx;

namespace Assets.Scripts
{
    public class PlayerSelectManager : MonoBehaviour
    {
        public string LevelToLoad;
        internal IObservable<int> Player1Status;
        internal IObservable<int> Player2Status;
        internal IObservable<int> Player3Status;
        internal IObservable<int> Player4Status;
        private IObservable<int> PlayersJoined;
        private IObservable<int> PlayersReady;

        private void Awake()
        {
            var ih = InputHandlerSingleton.Instance;
        }

        private void Start()
        {
            print("player select manager start ran");
            Player1Status =
                InputHandlerSingleton.Instance.Player1Attack.Where(i => i == 3)
                    .Select(p => 1)
                    .Merge(InputHandlerSingleton.Instance.Player1Jump.Select(_ => -1))
                    .Scan(0, (acc, currentValue) => Mathf.Clamp(acc + currentValue, 0, 2));
            Player2Status =
                InputHandlerSingleton.Instance.Player2Attack.Where(i => i == 3)
                    .Select(p => 1)
                    .Merge(InputHandlerSingleton.Instance.Player2Jump.Select(_ => -1))
                    .Scan(0, (acc, currentValue) => Mathf.Clamp(acc + currentValue, 0, 2));
            Player3Status =
                InputHandlerSingleton.Instance.Player3Attack.Where(i => i == 3)
                    .Select(p => 1)
                    .Merge(InputHandlerSingleton.Instance.Player3Jump.Select(_ => -1))
                    .Scan(0, (acc, currentValue) => Mathf.Clamp(acc + currentValue, 0, 2));
            Player4Status =
                InputHandlerSingleton.Instance.Player4Attack.Where(i => i == 3)
                    .Select(p => 1)
                    .Merge(InputHandlerSingleton.Instance.Player4Jump.Select(_ => -1))
                    .Scan(0, (acc, currentValue) => Mathf.Clamp(acc + currentValue, 0, 2));
            //Player1Status.Subscribe(i => print(i));
            Player3Status.Subscribe(i => print("player 3 status " + i));
            PlayersJoined =
                Player1Status.Select(s => s > 0 ? 1 : 0).StartWith(0).DistinctUntilChanged()
                    .CombineLatest(
                        Player2Status.Select(s => s > 0 ? 1 : 0).StartWith(0).DistinctUntilChanged(),
                        (acc, currentValue) => acc + currentValue)
                    .CombineLatest(
                        Player3Status.Select(s => s > 0 ? 1 : 0).StartWith(0).DistinctUntilChanged(),
                        (acc, currentValue) => acc + currentValue)
                    .CombineLatest(
                        Player4Status.Select(s => s > 0 ? 1 : 0).StartWith(0).DistinctUntilChanged(),
                        (acc, currentValue) => acc + currentValue);
            PlayersReady =
                Player1Status.Select(s => s > 1 ? 1 : 0).StartWith(0).DistinctUntilChanged()
                    .CombineLatest(
                        Player2Status.Select(s => s > 1 ? 1 : 0).StartWith(0).DistinctUntilChanged(),
                        (acc, currentValue) => acc + currentValue)
                    .CombineLatest(
                        Player3Status.Select(s => s > 1 ? 1 : 0).StartWith(0).DistinctUntilChanged(),
                        (acc, currentValue) => acc + currentValue)
                    .CombineLatest(
                        Player4Status.Select(s => s > 1 ? 1 : 0).StartWith(0).DistinctUntilChanged(),
                        (acc, currentValue) => acc + currentValue);
            //PlayersJoined.Subscribe(i => print("players joined " + i));
            //PlayersReady.Subscribe(i => print("players ready " + i));
            PlayersJoined.CombineLatest(PlayersReady, (joined, ready) => joined > 1 && ready == joined).Subscribe(t =>
            {
                if(t)
                    UnityEngine.SceneManagement.SceneManager.LoadScene(LevelToLoad);
            });
        }
    }
}
