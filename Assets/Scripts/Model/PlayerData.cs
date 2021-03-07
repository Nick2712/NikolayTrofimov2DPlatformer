namespace NikolayT2DGame
{
    public class PlayerData
    {
        public int Health
        {
            get
            {
                return _currentHealth;
            }
            set
            {
                _currentHealth = value;
                if (_currentHealth > _playerStartHealth) _currentHealth = _playerStartHealth;
                if (_currentHealth < 0) _currentHealth = 0;
            }
        }

        private int _currentHealth;
        private readonly int _playerStartHealth;

        public PlayerData(int playerStartHealth)
        {
            _playerStartHealth = playerStartHealth;
            _currentHealth = playerStartHealth;
        }
    }
}