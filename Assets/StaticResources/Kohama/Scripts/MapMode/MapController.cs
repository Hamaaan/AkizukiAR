using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    /// <summary>
    /// Google API Key & Base URL
    /// </summary>
    [SerializeField] string GoogleApiKey = "AIzaSyA3xYXg6YYrFds-Xia571u9yakxcjD4lGk";  // �ݒ肵�Ă�������
    [SerializeField] string BaseUrl = @"https://maps.googleapis.com/maps/api/staticmap?";

    /// <summary>
    /// �ܓx�i�o�x�j�P�x�̋����im�j
    /// </summary>
    private const float Lat2Meter = 111319.491f;

    /// <summary>
    /// �}�b�v�X�V��臒l
    /// </summary>
    [SerializeField] const float ThresholdDistance = 1f;

    /// <summary>
    /// �}�b�v�X�V����
    /// </summary>
    private const float UpdateMapTime = 5f;

    /// <summary>
    /// �_�E�����[�h����}�b�v�C���[�W�̃T�C�Y
    /// </summary>
    private const int MapImageSize = 640;

    /// <summary>
    /// ��ʂɕ\������}�b�v�X�v���C�g�̃T�C�Y
    /// </summary>
    private const int MapSpriteSize = 960;

    [SerializeField] GameObject loading;        // �_�E�����[�h�m�F�p�I�u�W�F�N�g
    [SerializeField] Text txtLocation;    // ���W
    [SerializeField] Text txtDistance;    // ����
    [SerializeField] Image mapImage;       // �}�b�v Image
    [SerializeField] Cursor cursor;         // �J�[�\��
    [SerializeField] float zoom = 18;         // �J�[�\��

    /// <summary>
    /// �N��������
    /// </summary>
    void Start()
    {
        // ���[�f�B���O�\�����\���ɂ��Ă���
        loading.SetActive(false);
        updateDistance(0f);

        // GPS ������
        Input.location.Start();
        Input.compass.enabled = true;

        // �}�b�v�擾
        StartCoroutine(updateMap());
    }

    /// <summary>
    /// �}�b�v�X�V
    /// </summary>
    /// <returns></returns>
    private IEnumerator updateMap()
    {
        Debug.Log("upadateMap");

        // GPS ��������Ă��Ȃ�
        if (!Input.location.isEnabledByUser) yield break;

        // �T�[�r�X�̏�Ԃ��N�����ɂȂ�܂őҋ@
        while (Input.location.status != LocationServiceStatus.Running)
        {
            yield return new WaitForSeconds(2f);
        }

        // �J�[�\�����A�N�e�B�u��
        cursor.IsEnabled = true;

        LocationInfo curr;
        LocationInfo prev = new LocationInfo();
        while (true)
        {
            // ���݈ʒu
            curr = Input.location.lastData;
            txtLocation.text = string.Format("�ܓx�F{0:0.000000}, �o�x�F{1:0.000000}", curr.latitude, curr.longitude);

            // ���ȏ�ړ����Ă���
            if (getDistanceFromLocation(curr, prev) >= ThresholdDistance)
            {
                // �}�b�v������
                yield return StartCoroutine(downloading(curr));
                prev = curr;
            }

            // �ҋ@
            yield return new WaitForSeconds(UpdateMapTime);
        }
    }

    /// <summary>
    /// �}�b�v�摜�_�E�����[�h
    /// </summary>
    /// <param name="curr">���݂̍��W</param>
    /// <returns>�R���[�`��</returns>
    private IEnumerator downloading(LocationInfo curr)
    {
        loading.SetActive(true);
        Debug.Log("Start");

        // �x�[�X URL
        string url = BaseUrl;
        // ���S���W
        url += "center=" + curr.latitude + "," + curr.longitude;
        // �Y�[��
        url += "&zoom=" + zoom;   // �f�t�H���g 0 �Ȃ̂ŁA�K���ȃT�C�Y�ɐݒ�
        // �摜�T�C�Y�i640x640������j
        url += "&size=" + MapImageSize + "x" + MapImageSize;
        // API Key
        url += "&key=" + GoogleApiKey;

        // �n�}�摜���_�E�����[�h
        url = UnityWebRequest.UnEscapeURL(url);
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);
        yield return req.SendWebRequest();

        // �e�N�X�`������
        if (req.error == null) 
        { 
            yield return StartCoroutine(updateSprite(req.downloadHandler.data));
        }
        else
        {
            Debug.Log("error");
        }

        updateDistance(0f);
        loading.SetActive(false);
    }

    /// <summary>
    /// �X�v���C�g�̍X�V
    /// </summary>
    /// <param name="data">�}�b�v�摜�f�[�^</param>
    /// <returns>�R���[�`��</returns>
    private IEnumerator updateSprite(byte[] data)
    {
        // �e�N�X�`������
        Texture2D tex = new Texture2D(MapSpriteSize, MapSpriteSize);
        tex.LoadImage(data);
        if (tex == null) yield break;
        // �X�v���C�g�i�C���X�^���X�j�𖾎��I�ɊJ��
        if (mapImage.sprite != null)
        {
            Destroy(mapImage.sprite);
            yield return null;
            mapImage.sprite = null;
            yield return null;
        }
        // �X�v���C�g�i�C���X�^���X�j�𓮓I�ɐ���
        mapImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    /// <summary>
    /// 2�_�Ԃ̋������擾����
    /// </summary>
    /// <param name="curr">���݂̍��W</param>
    /// <param name="prev">���O�̍��W</param>
    /// <returns>�����i���[�g���j</returns>
    private float getDistanceFromLocation(LocationInfo curr, LocationInfo prev)
    {
        Vector3 cv = new Vector3((float)curr.longitude, 0, (float)curr.latitude);
        Vector3 pv = new Vector3((float)prev.longitude, 0, (float)prev.latitude);
        float dist = Vector3.Distance(cv, pv) * Lat2Meter;
        updateDistance(dist);
        return dist;
    }

    /// <summary>
    /// �����\��
    /// </summary>
    /// <param name="dist">����</param>
    private void updateDistance(float dist)
    {
        txtDistance.text = string.Format("�����F{0:0.0000} m", dist);
    }

}