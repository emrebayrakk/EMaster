import React from "react";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from "recharts";

const MonthlyChart = ({ data, title }) => {
  if (!data || data.length === 0) {
    return <p>No data available for {title.toLowerCase()}.</p>;
  }

  const groupedData = data.reduce((acc, { month, category, amount }) => {
    if (!acc[month]) acc[month] = {};
    acc[month][category] = amount;
    return acc;
  }, {});

  const formattedData = Object.keys(groupedData).map((month) => ({
    month,
    ...groupedData[month],
  }));

  const categories = Array.from(new Set(data.map((item) => item.category)));
  const colors = ["#8884d8", "#82ca9d", "#ffc658", "#ff7300", "#a4de6c"];

  return (
    <div style={styles.chartWrapper}>
      <h3 style={styles.chartTitle}>{title}</h3>
      <ResponsiveContainer width="100%" height={400}>
        <BarChart data={formattedData}>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="month" />
          <YAxis />
          <Tooltip />
          <Legend />
          {categories.map((category, index) => (
            <Bar
              key={category}
              dataKey={category}
              fill={colors[index % colors.length]}
              name={category}
              barSize={20}
            />
          ))}
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
};

const styles = {
  chartWrapper: {
    width: "100%",
    backgroundColor: "#fff",
    borderRadius: "8px",
    boxShadow: "0 2px 10px rgba(0,0,0,0.1)",
    padding: "20px",
  },
  chartTitle: {
    fontSize: "18px",
    fontWeight: "600",
    marginBottom: "15px",
  },
};

export default MonthlyChart;
