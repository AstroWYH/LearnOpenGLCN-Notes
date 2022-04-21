#version 330 core
layout (triangles) in; // wyh gs的in, 是三角形, 应该是模型直接提供的, 不然一般我们自定义的话是point
layout (line_strip, max_vertices = 6) out; // wyh gs的out, 绘制的是lines

in VS_OUT {
    vec3 normal; // wyh
} gs_in[];

const float MAGNITUDE = 0.2; // wyh 控制绘制的法线的长度

uniform mat4 projection;

void GenerateLine(int index) // wyh
{
    gl_Position = projection * gl_in[index].gl_Position; // wyh 绘制法线可视化起点
    EmitVertex();
    gl_Position = projection * (gl_in[index].gl_Position + vec4(gs_in[index].normal, 0.0) * MAGNITUDE); // wyh 绘制法线可视化终点
    EmitVertex();
    EndPrimitive();
}

void main()
{
    GenerateLine(0); // first vertex normal // wyh gs的in三角形的第1个顶点的法线
    GenerateLine(1); // second vertex normal
    GenerateLine(2); // third vertex normal
}