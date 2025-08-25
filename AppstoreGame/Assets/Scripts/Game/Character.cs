using System.Collections;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _hand;
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private GameObject _shootLine;
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _lose;
    private Arrow _selectedArrow;
    [SerializeField] private TextMeshProUGUI _arrowDisplay;
    private int _arrowCount;
    private bool _isMoving;
    private Vector3 _targetPosition;
    private int _currentPipe;
    private int _pipeCount;
    private bool _canArrow;
    public void Initialize(Sprite skin, int pipeCount)
    {
        _body.GetComponent<SpriteRenderer>().sprite = skin;
        _arrowCount = 10;
        _arrowDisplay.text = $"{_arrowCount}/10";
        _currentPipe = 0;
        _pipeCount = pipeCount;
        _canArrow = true;
        Arrow.OnEnd += MoveNextPipe;
    }

    private void Update()
    {
        if (_isMoving)
        {
            if(Vector3.Distance(transform.position, _targetPosition) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 5 * Time.deltaTime);
            }
            else
            {
                _isMoving = false;
                _canArrow = true;
                gameObject.GetComponent<Animator>().Play("CharacterIdle");
            }
        }
    }
    private void OnDestroy()
    {
        Arrow.OnEnd -= MoveNextPipe;
    }
    public void OnStart(Vector3 targetPosition)
    {
        if (_canArrow)
        {
            RotateTo(targetPosition);
            TakeArrow();
        }
    }

    public void OnDragging(Vector3 targetPosition)
    {
        RotateTo(targetPosition);
    }

    public void OnEnded()
    {
        if(_selectedArrow != null)
        {
            _selectedArrow.PushArrow();
            _canArrow = false;
            _selectedArrow = null;
            _hand.GetComponent<Animator>().Play("HandIdle");
            _shootLine.SetActive(false);

            _arrowCount--;
            _arrowDisplay.text = $"{_arrowCount}/10";
        }
    }

    public void RotateTo(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - _body.position;
        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, -50f, 60f);

        _body.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void TakeArrow()
    {
        if (_arrowCount == 0)
            return;
        _selectedArrow = Instantiate(_arrowPrefab, _hand);
        SFXController.Instance.Play(SoundType.Draw);
        _selectedArrow.transform.localPosition = Vector3.zero + new Vector3(6f, 0, 0);
        _hand.GetComponent<Animator>().Play("HandArrow");
        _shootLine.SetActive(true);
    }

    private void MoveNextPipe(bool ale)
    {
        if(_arrowCount == 0)
        {
            StartCoroutine(Loseedelay());
        }
        else if(ale)
            StartCoroutine(MoveDelay());
        else
        {
            _canArrow = true;
        }
    }

    private IEnumerator MoveDelay()
    {
        _body.rotation = Quaternion.Euler(0f, 0f, 0f);
        yield return new WaitForSeconds(1f);
        if (_currentPipe < _pipeCount)
        {
            _currentPipe++;
            _targetPosition = new Vector3(transform.position.x + 13, transform.position.y, transform.position.z);
            _isMoving = true;
            gameObject.GetComponent<Animator>().Play("CharacterMove");
        }
        else
        {
            if(_win.activeSelf == false)
            {
                _lose.SetActive(true);
            }
        }
    }

    private IEnumerator Loseedelay()
    {
        yield return new WaitForSeconds(2f);
        if (_win.activeSelf == false)
        {
            _lose.SetActive(true);
        }
    }
}
