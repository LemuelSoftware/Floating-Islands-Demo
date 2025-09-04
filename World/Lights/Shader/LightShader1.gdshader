shader_type spatial;
render_mode cull_disabled, vertex_lighting;

uniform vec3 glow_color: source_color = vec3(0.5,1,1);
uniform float glow_power: hint_range(0.0, 10.0, 0.1);

void fragment() {
	ALBEDO = glow_color;
	EMISSION = glow_color * glow_power;
}
