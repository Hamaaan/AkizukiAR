using UnityEngine;

/// <summary>
/// �J�[�\������
/// </summary>
public class Cursor : MonoBehaviour
{
    public bool IsEnabled { get; set; }

    /// <summary>
    /// �N��
    /// </summary>
    void Start()
    {
        IsEnabled = false;
    }

    /// <summary>
    /// �X�V
    /// </summary>
    void Update()
    {
        if (IsEnabled)
        {
            // �J�[�\���̌��������킹��
            this.transform.localEulerAngles = new Vector3(0, 0, 360 - Input.compass.trueHeading);
        }
    }
}