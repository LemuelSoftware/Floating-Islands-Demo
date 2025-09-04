shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D lightning_noise_1;
uniform sampler2D lightning_noise_2;
uniform vec3 tint : source_color;
uniform float distortion_vertex : hint_range(0.0, 0.3, 0.005) = 0.03;
uniform float speed_vertex : hint_range(0.0, 1.0, 0.005) = 0.5;
uniform sampler2D noise_vertex;

void vertex()
{
	float noise_val = (texture(noise_vertex, UV + (TIME * speed_vertex)).r * 2.0) - 1.0;
	vec3 displacement = NORMAL * noise_val * distortion_vertex;
	VERTEX += displacement;
}

void fragment()
{
	float layer1 = texture(lightning_noise_1, UV + (TIME * 0.1)).r * sin(TIME * 2.0);
	float layer2 = texture(lightning_noise_2, UV - (TIME * 0.1)).r * cos(TIME * 2.0);
	ALBEDO = vec3(layer1 + layer2) * tint;
	ALPHA = layer1 + layer2;
}