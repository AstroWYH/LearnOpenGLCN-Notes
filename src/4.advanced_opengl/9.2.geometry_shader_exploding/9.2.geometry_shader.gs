#version 330 core
layout (triangles) in; // wyh gs的输入就是三角形, 怎么理解?
layout (triangle_strip, max_vertices = 3) out; // wyh

in VS_OUT {
    vec2 texCoords;
} gs_in[]; // wyh 9.1是颜色, 9.2换成了纹理坐标

out vec2 TexCoords; 

uniform float time;

vec4 explode(vec4 position, vec3 normal) // wyh 位置向量沿着法线向量进行位移之后的结果
{
    float magnitude = 2.0; // wyh magnitude是为了控制爆炸膨胀后的顶点位置不要太远
    vec3 direction = normal * ((sin(time) + 1.0) / 2.0) * magnitude; // wyh 随着时间变化, 是否和凹凸贴图一回事?
    return position + vec4(direction, 0.0);
}

vec3 GetNormal() // wyh
{
    vec3 a = vec3(gl_in[0].gl_Position) - vec3(gl_in[1].gl_Position);
    vec3 b = vec3(gl_in[2].gl_Position) - vec3(gl_in[1].gl_Position);
    return normalize(cross(a, b)); // wyh 求每个顶点的法线
}

void main() {    
    vec3 normal = GetNormal(); // wyh

    gl_Position = explode(gl_in[0].gl_Position, normal); // wyh 顶点 + 法线向量 = 另一个位置的顶点
    TexCoords = gs_in[0].texCoords; // wyh 纹理采样, 没什么花样; gs_in定义的, gl_in是glsl内建变量
    EmitVertex();
    gl_Position = explode(gl_in[1].gl_Position, normal);
    TexCoords = gs_in[1].texCoords;
    EmitVertex();
    gl_Position = explode(gl_in[2].gl_Position, normal);
    TexCoords = gs_in[2].texCoords;
    EmitVertex(); // wyh
    EndPrimitive(); // wyh
}