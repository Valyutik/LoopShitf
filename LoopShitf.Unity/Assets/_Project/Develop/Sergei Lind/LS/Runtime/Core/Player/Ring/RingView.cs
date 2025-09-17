using UnityEngine;

namespace Sergei_Lind.LS.Runtime.Core.Player.Ring
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public sealed class RingView : MonoBehaviour
    {
        private int _segments = 64;
        private float _innerRadius;
        private float _outerRadius;
        private Color _color = Color.gray;

        public void Generate(float radius, float thickness, float playerRingOffset, int segmentsCount, string colorHex)
        {
            _segments = segmentsCount;
            _innerRadius = radius - thickness * playerRingOffset;
            _outerRadius = radius + thickness * playerRingOffset;

            var mesh = new Mesh();
            var vertices = new Vector3[(_segments + 1) * 2];
            var uv = new Vector2[vertices.Length];
            var triangles = new int[_segments * 6];

            var angleStep = 2 * Mathf.PI / _segments;
            for (var i = 0; i <= _segments; i++)
            {
                var angle = i * angleStep;
                var cos = Mathf.Cos(angle);
                var sin = Mathf.Sin(angle);

                vertices[i * 2] = new Vector3(cos * _outerRadius, sin * _outerRadius, 0);
                uv[i * 2] = new Vector2((float)i / _segments, 1);

                vertices[i * 2 + 1] = new Vector3(cos * _innerRadius, sin * _innerRadius, 0);
                uv[i * 2 + 1] = new Vector2((float)i / _segments, 0);

                if (i >= _segments) continue;
                var start = i * 2;
                var triIndex = i * 6;
                triangles[triIndex] = start;
                triangles[triIndex + 1] = start + 2;
                triangles[triIndex + 2] = start + 1;

                triangles[triIndex + 3] = start + 2;
                triangles[triIndex + 4] = start + 3;
                triangles[triIndex + 5] = start + 1;
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            GetComponent<MeshFilter>().mesh = mesh;

            ColorUtility.TryParseHtmlString(colorHex, out _color);
            var material = new Material(Shader.Find("Sprites/Default"))
            {
                color = _color
            };
            GetComponent<MeshRenderer>().material = material;
        }
    }
}